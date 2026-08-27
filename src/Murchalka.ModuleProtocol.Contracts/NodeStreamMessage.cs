using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Multiplexes resumable control and event traffic over the authenticated Node channel.</summary>
/// <param name="MessageId">The message identifier.</param>
/// <param name="NodeId">The authenticated Node identity.</param>
/// <param name="Kind">The message kind.</param>
/// <param name="Sequence">The sender-local monotonic sequence.</param>
/// <param name="AcknowledgesThrough">The peer sequence durably processed by the sender.</param>
/// <param name="OccurredAt">The sender time.</param>
/// <param name="Payload">The kind-specific bounded JSON payload.</param>
public sealed record NodeStreamMessage(Guid MessageId, NodeId NodeId, NodeStreamMessageKind Kind, long Sequence,
    long AcknowledgesThrough, DateTimeOffset OccurredAt, JsonElement Payload);
