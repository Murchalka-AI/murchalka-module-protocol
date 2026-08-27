namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Defines a Node task execution state.</summary>
public enum NodeTaskState
{
    /// <summary>The state is not recognized by this protocol version.</summary>
    Unknown,
    /// <summary>The task passed validation and is queued.</summary>
    Accepted,
    /// <summary>The task is executing.</summary>
    Running,
    /// <summary>The task completed successfully.</summary>
    Succeeded,
    /// <summary>The task failed.</summary>
    Failed,
    /// <summary>The task was cancelled.</summary>
    Cancelled,
    /// <summary>The task was rejected before execution.</summary>
    Rejected
}
