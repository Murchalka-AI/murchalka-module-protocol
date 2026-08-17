namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Defines the tenancy and execution scope of an invocation.</summary>
/// <param name="TenantId">The tenant identifier.</param><param name="WorkspaceId">The workspace identifier.</param>
/// <param name="PersonId">The person identifier.</param><param name="GroupId">The group identifier.</param>
/// <param name="SessionId">The session identifier.</param><param name="NodeId">The node identifier.</param>
public sealed record InvocationScope(string? TenantId, string? WorkspaceId, string? PersonId,
    string? GroupId, string? SessionId, string? NodeId);
