using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains a capability invocation request and its security context.</summary>
/// <param name="InvocationId">The invocation identifier.</param><param name="CapabilityId">The capability identifier.</param>
/// <param name="CapabilityVersion">The capability version.</param><param name="ProviderInstance">The provider instance.</param>
/// <param name="ConsumerModuleId">The consumer module.</param><param name="ActorReference">The actor reference.</param>
/// <param name="Scope">The invocation scope.</param><param name="Purpose">The declared purpose.</param>
/// <param name="AuthorizationGrantReference">The authorization grant reference.</param><param name="TraceId">The trace identifier.</param>
/// <param name="CorrelationId">The correlation identifier.</param><param name="CausationId">The causation identifier.</param>
/// <param name="Deadline">The deadline.</param><param name="IdempotencyKey">The idempotency key.</param>
/// <param name="PayloadSchema">The payload schema identifier.</param><param name="Payload">The inline payload.</param>
/// <param name="ContentReference">The external content reference.</param>
public sealed record InvocationEnvelope(Guid InvocationId, CapabilityId CapabilityId, SemanticVersion CapabilityVersion,
    InstanceId ProviderInstance, ModuleId ConsumerModuleId, string? ActorReference, InvocationScope Scope, string Purpose,
    string AuthorizationGrantReference, string TraceId, string CorrelationId, string? CausationId, DateTimeOffset Deadline,
    string? IdempotencyKey, string PayloadSchema, JsonElement? Payload, Uri? ContentReference);
