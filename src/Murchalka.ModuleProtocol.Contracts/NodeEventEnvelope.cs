using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains a signed, sequenced event emitted by a Node module.</summary>
/// <param name="EventId">The event identifier.</param>
/// <param name="NodeId">The producing Node.</param>
/// <param name="ProducerModuleId">The producing module.</param>
/// <param name="ProviderInstance">The producing provider instance.</param>
/// <param name="Topic">The event topic.</param>
/// <param name="SchemaVersion">The event schema version.</param>
/// <param name="Sequence">The Node-wide monotonic event sequence.</param>
/// <param name="OccurredAt">The occurrence time.</param>
/// <param name="DataClassification">The data classification.</param>
/// <param name="PayloadSchema">The payload schema digest.</param>
/// <param name="Payload">The bounded event payload.</param>
/// <param name="Signature">The Base64 Node signature.</param>
public sealed record NodeEventEnvelope(Guid EventId, NodeId NodeId, ModuleId ProducerModuleId,
    InstanceId ProviderInstance, string Topic, int SchemaVersion, long Sequence, DateTimeOffset OccurredAt,
    DataClassification DataClassification, string PayloadSchema, JsonElement Payload, string Signature);
