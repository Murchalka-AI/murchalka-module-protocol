using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains a versioned event and its provenance metadata.</summary>
/// <param name="EventId">The event identifier.</param><param name="Topic">The event topic.</param>
/// <param name="SchemaVersion">The schema version.</param><param name="ProducerModule">The producer module.</param>
/// <param name="ProducerInstance">The producer instance.</param><param name="OccurredAt">The occurrence time.</param>
/// <param name="PublishedAt">The publication time.</param><param name="TenantId">The tenant identifier.</param>
/// <param name="ActorReference">The actor reference.</param><param name="CorrelationId">The correlation identifier.</param>
/// <param name="CausationId">The causation identifier.</param><param name="PartitionKey">The partition key.</param>
/// <param name="DataClassification">The data classification.</param><param name="Purpose">The declared purpose.</param>
/// <param name="PayloadSchema">The payload schema identifier.</param><param name="Payload">The event payload.</param>
public sealed record EventEnvelope(Guid EventId, string Topic, int SchemaVersion, ModuleId ProducerModule,
    InstanceId ProducerInstance, DateTimeOffset OccurredAt, DateTimeOffset PublishedAt, string? TenantId,
    string? ActorReference, string CorrelationId, string? CausationId, string PartitionKey,
    DataClassification DataClassification, string Purpose, string PayloadSchema, JsonElement Payload);
