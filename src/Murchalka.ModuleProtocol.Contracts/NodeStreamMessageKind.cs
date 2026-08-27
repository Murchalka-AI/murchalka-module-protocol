namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Defines the multiplexed Node stream message kind.</summary>
public enum NodeStreamMessageKind
{
    /// <summary>The kind is not recognized by this protocol version.</summary>
    Unknown,
    /// <summary>A full Node descriptor snapshot.</summary>
    Descriptor,
    /// <summary>A capability task command.</summary>
    Task,
    /// <summary>A task progress or result update.</summary>
    TaskUpdate,
    /// <summary>A task cancellation command.</summary>
    CancelTask,
    /// <summary>A Node-originated event.</summary>
    Event,
    /// <summary>An immutable module bundle deployment.</summary>
    ModuleBundle,
    /// <summary>A module deployment status update.</summary>
    DeploymentStatus,
    /// <summary>A liveness heartbeat.</summary>
    Heartbeat,
    /// <summary>An acknowledgement carrying the last durable sequence.</summary>
    Acknowledgement
}
