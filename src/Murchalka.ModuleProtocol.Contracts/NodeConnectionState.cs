namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Defines the observed Node connection state.</summary>
public enum NodeConnectionState
{
    /// <summary>The state is not recognized by this protocol version.</summary>
    Unknown,
    /// <summary>The Node has not completed enrollment.</summary>
    PendingEnrollment,
    /// <summary>The Node is connected and authenticated.</summary>
    Connected,
    /// <summary>The Node is temporarily disconnected.</summary>
    Offline,
    /// <summary>The Node identity has been revoked.</summary>
    Revoked
}
