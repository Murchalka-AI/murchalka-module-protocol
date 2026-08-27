namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Defines a Node enrollment request state.</summary>
public enum NodeEnrollmentState
{
    /// <summary>The state is not recognized by this protocol version.</summary>
    Unknown,
    /// <summary>The request awaits administrator approval.</summary>
    PendingApproval,
    /// <summary>The request was approved and a certificate is available.</summary>
    Approved,
    /// <summary>The request was rejected.</summary>
    Rejected,
    /// <summary>The request expired before approval.</summary>
    Expired
}
