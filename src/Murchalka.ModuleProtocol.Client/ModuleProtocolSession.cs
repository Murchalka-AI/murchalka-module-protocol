using Murchalka.ModuleProtocol.Contracts;

namespace Murchalka.ModuleProtocol.Client;

/// <summary>Enforces the module-side activation and control state machine.</summary>
public sealed class ModuleProtocolSession
{
    private readonly ModuleHello _hello;
    private readonly IModuleProofProvider _proofProvider;
    private RuntimeChallenge? _challenge;
    private bool _configurationReceived;
    private bool _grantReceived;
    private bool _dependenciesReceived;

    /// <summary>Initializes a module protocol session.</summary>
    public ModuleProtocolSession(ModuleHello hello, IModuleProofProvider proofProvider)
    {
        ArgumentNullException.ThrowIfNull(hello);
        ArgumentNullException.ThrowIfNull(proofProvider);
        ValidateHello(hello);
        _hello = hello;
        _proofProvider = proofProvider;
    }

    /// <summary>Gets the current session state.</summary>
    public ModuleSessionState State { get; private set; } = ModuleSessionState.Created;
    /// <summary>Gets the terminal protocol fault, if any.</summary>
    public ProtocolFault? Fault { get; private set; }

    /// <summary>Transitions the session by creating the hello message.</summary>
    public ModuleHello CreateHello() { RequireState(ModuleSessionState.Created); State = ModuleSessionState.HelloSent; return _hello; }

    /// <summary>Validates and accepts a runtime challenge.</summary>
    public void AcceptChallenge(RuntimeChallenge challenge, DateTimeOffset now)
    {
        RequireState(ModuleSessionState.HelloSent);
        try
        {
            if (!_hello.ProtocolVersions.Contains(challenge.SelectedProtocolVersion))
                throw new ProtocolNegotiationException("protocol-version-unsupported", "Runtime selected a protocol that the module did not offer.");
            if (!string.Equals(_hello.Nonce, challenge.ModuleNonce, StringComparison.Ordinal))
                throw new ProtocolNegotiationException("challenge-nonce-mismatch", "Runtime challenge does not bind the module nonce.");
            if (string.IsNullOrWhiteSpace(challenge.RuntimeNonce))
                throw new ProtocolNegotiationException("challenge-nonce-missing", "Runtime challenge nonce is required.");
            if (challenge.ExpiresAt <= now || challenge.IssuedAt > now.AddMinutes(1))
                throw new ProtocolNegotiationException("challenge-expired", "Runtime challenge is expired or not yet valid.");
            if (!string.Equals(challenge.ProofAlgorithm, "hmac-sha256", StringComparison.Ordinal))
                throw new ProtocolNegotiationException("proof-algorithm-unsupported", "The proof algorithm is not supported.");
            _challenge = challenge;
            State = ModuleSessionState.ChallengeReceived;
        }
        catch (ProtocolNegotiationException exception) { Fail(exception.Code, exception.Message); throw; }
    }

    /// <summary>Creates the authentication proof for the accepted challenge.</summary>
    public ModuleProof CreateProof()
    {
        RequireState(ModuleSessionState.ChallengeReceived);
        var challenge = _challenge!;
        var proof = new ModuleProof(_hello.ModuleId, _hello.InstanceId, challenge.RuntimeNonce,
            challenge.ModuleNonce, _proofProvider.CreateProof(_hello, challenge));
        State = ModuleSessionState.ProofSent;
        return proof;
    }

    /// <summary>Applies the activation configuration snapshot.</summary>
    public void ApplyConfiguration(ConfigurationSnapshot snapshot)
    { RequireSnapshotState(); ArgumentNullException.ThrowIfNull(snapshot); _configurationReceived = true; UpdateSnapshotState(); }

    /// <summary>Validates and applies the activation permission grant.</summary>
    public void ApplyGrant(PermissionGrantSnapshot snapshot, DateTimeOffset now)
    {
        RequireSnapshotState(); ArgumentNullException.ThrowIfNull(snapshot);
        if (!string.Equals(snapshot.BundleDigest, _hello.BundleDigest, StringComparison.Ordinal))
        { Fail("grant-bundle-mismatch", "Permission grant is bound to another bundle digest."); throw new ProtocolNegotiationException("grant-bundle-mismatch", "Permission grant is bound to another bundle digest."); }
        if (snapshot.ExpiresAt <= now)
        { Fail("grant-expired", "Permission grant has expired."); throw new ProtocolNegotiationException("grant-expired", "Permission grant has expired."); }
        _grantReceived = true; UpdateSnapshotState();
    }

    /// <summary>Applies the activation dependency bindings.</summary>
    public void ApplyDependencies(DependencyEndpointsSnapshot snapshot)
    { RequireSnapshotState(); ArgumentNullException.ThrowIfNull(snapshot); _dependenciesReceived = true; UpdateSnapshotState(); }

    /// <summary>Creates the module-ready message.</summary>
    public ModuleReady CreateReady(DateTimeOffset now)
    { RequireState(ModuleSessionState.SnapshotsReceived); State = ModuleSessionState.ReadySent; return new ModuleReady(_hello.ModuleId, _hello.InstanceId, _hello.DeclaredCapabilitiesDigest, now); }

    /// <summary>Validates and applies a runtime control message.</summary>
    public void AcceptControl(ControlMessage message, DateTimeOffset now)
    {
        ArgumentNullException.ThrowIfNull(message);
        if (message.Deadline <= now) throw new TimeoutException($"Control operation '{message.OperationId}' exceeded its deadline.");
        switch (message.Kind)
        {
            case ControlMessageKind.Activate when State == ModuleSessionState.ReadySent: State = ModuleSessionState.Active; break;
            case ControlMessageKind.Drain when State == ModuleSessionState.Active: State = ModuleSessionState.Draining; break;
            case ControlMessageKind.Stop when State is ModuleSessionState.Active or ModuleSessionState.Draining or ModuleSessionState.ReadySent: State = ModuleSessionState.Stopped; break;
            case ControlMessageKind.ReloadConfiguration or ControlMessageKind.UpdateBindings or ControlMessageKind.UpdateGrant or ControlMessageKind.HealthProbe when State == ModuleSessionState.Active: break;
            case ControlMessageKind.ExportState or ControlMessageKind.PrepareMigration or ControlMessageKind.CommitMigration or ControlMessageKind.RollbackMigration when State is ModuleSessionState.Active or ModuleSessionState.Draining: break;
            default:
                var prior = State; Fail("invalid-state-transition", $"Control '{message.Kind}' is invalid while session is '{prior}'.");
                throw new InvalidOperationException(Fault!.Message);
        }
    }

    private static void ValidateHello(ModuleHello hello)
    {
        if (hello.ProtocolVersions.Count == 0 || hello.ProtocolVersions.Any(version => version <= 0)) throw new ArgumentException("At least one positive protocol major must be offered.", nameof(hello));
        if (!IsSha256Digest(hello.BundleDigest)) throw new ArgumentException("Bundle digest must be a sha256 digest.", nameof(hello));
        if (!IsSha256Digest(hello.DeclaredCapabilitiesDigest)) throw new ArgumentException("Capabilities digest must be a sha256 digest.", nameof(hello));
        if (string.IsNullOrWhiteSpace(hello.Nonce)) throw new ArgumentException("Module nonce is required.", nameof(hello));
    }

    private static bool IsSha256Digest(string digest) => digest.Length == 71 && digest.StartsWith("sha256:", StringComparison.Ordinal) && digest[7..].All(character => character is >= '0' and <= '9' or >= 'a' and <= 'f');
    private void RequireSnapshotState() { if (State is not (ModuleSessionState.ProofSent or ModuleSessionState.SnapshotsReceived)) RequireState(ModuleSessionState.ProofSent); }
    private void UpdateSnapshotState() { if (_configurationReceived && _grantReceived && _dependenciesReceived) State = ModuleSessionState.SnapshotsReceived; }
    private void RequireState(ModuleSessionState expected) { if (State == expected) return; var prior = State; Fail("invalid-state-transition", $"Expected session state '{expected}', actual '{prior}'."); throw new InvalidOperationException(Fault!.Message); }
    private void Fail(string code, string message) { Fault = new ProtocolFault(code, message); State = ModuleSessionState.Faulted; }
}
