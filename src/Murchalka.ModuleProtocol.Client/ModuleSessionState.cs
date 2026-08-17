namespace Murchalka.ModuleProtocol.Client;

/// <summary>Describes the state of a module protocol session.</summary>
public enum ModuleSessionState
{
    /// <summary>The session was created.</summary>
    Created,
    /// <summary>The hello message was sent.</summary>
    HelloSent,
    /// <summary>The runtime challenge was received.</summary>
    ChallengeReceived,
    /// <summary>The module proof was sent.</summary>
    ProofSent,
    /// <summary>All activation snapshots were received.</summary>
    SnapshotsReceived,
    /// <summary>The ready message was sent.</summary>
    ReadySent,
    /// <summary>The module is active.</summary>
    Active,
    /// <summary>The module is draining.</summary>
    Draining,
    /// <summary>The module is stopped.</summary>
    Stopped,
    /// <summary>The session has faulted.</summary>
    Faulted
}
