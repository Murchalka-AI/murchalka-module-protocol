namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes the outcome of an invocation.</summary>
public enum InvocationStatus
{
    /// <summary>The invocation succeeded.</summary>
    Succeeded,
    /// <summary>The invocation failed.</summary>
    Failed,
    /// <summary>The invocation was cancelled.</summary>
    Cancelled,
    /// <summary>The invocation exceeded its deadline.</summary>
    DeadlineExceeded,
    /// <summary>The invocation was rejected before execution.</summary>
    Rejected
}
