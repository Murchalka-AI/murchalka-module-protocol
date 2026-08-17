namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Challenges a module to prove possession of its credential.</summary>
/// <param name="SelectedProtocolVersion">The negotiated protocol version.</param><param name="RuntimeNonce">The runtime nonce.</param>
/// <param name="ModuleNonce">The module nonce.</param><param name="ProofAlgorithm">The proof algorithm.</param>
/// <param name="IssuedAt">The issue time.</param><param name="ExpiresAt">The expiration time.</param>
public sealed record RuntimeChallenge(int SelectedProtocolVersion, string RuntimeNonce, string ModuleNonce,
    string ProofAlgorithm, DateTimeOffset IssuedAt, DateTimeOffset ExpiresAt);
