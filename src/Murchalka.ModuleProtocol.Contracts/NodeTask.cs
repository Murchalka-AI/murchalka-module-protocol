using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains an authenticated, deadline-bound capability task for one Node.</summary>
/// <param name="TaskId">The task identifier.</param>
/// <param name="NodeId">The target Node.</param>
/// <param name="ModuleId">The target Node module.</param>
/// <param name="ModuleVersion">The target module version.</param>
/// <param name="CapabilityId">The target capability.</param>
/// <param name="CapabilityVersion">The target capability version.</param>
/// <param name="ProviderInstance">The target provider instance.</param>
/// <param name="ActorReference">The authenticated actor reference.</param>
/// <param name="Purpose">The declared action purpose.</param>
/// <param name="ActionGrant">The signed bounded action grant.</param>
/// <param name="Arguments">The schema-validated arguments.</param>
/// <param name="AllowedPaths">The local filesystem intersection.</param>
/// <param name="AllowedNetwork">The local network intersection.</param>
/// <param name="SecretHandles">The opaque short-lived secret handles.</param>
/// <param name="ResourceBudget">The hard resource budget.</param>
/// <param name="Deadline">The task deadline.</param>
/// <param name="IdempotencyKey">The required idempotency key.</param>
/// <param name="TraceId">The distributed trace identifier.</param>
public sealed record NodeTask(Guid TaskId, NodeId NodeId, ModuleId ModuleId, SemanticVersion ModuleVersion,
    CapabilityId CapabilityId, SemanticVersion CapabilityVersion, InstanceId ProviderInstance, string ActorReference,
    string Purpose, NodeActionGrant ActionGrant, JsonElement Arguments, IReadOnlyList<string> AllowedPaths,
    IReadOnlyList<string> AllowedNetwork, IReadOnlyList<string> SecretHandles, NodeResourceBudget ResourceBudget,
    DateTimeOffset Deadline, string IdempotencyKey, string TraceId);
