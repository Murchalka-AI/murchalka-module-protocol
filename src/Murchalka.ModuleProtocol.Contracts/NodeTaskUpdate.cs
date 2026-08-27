using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Reports durable Node task progress or completion.</summary>
/// <param name="TaskId">The task identifier.</param>
/// <param name="State">The current state.</param>
/// <param name="Sequence">The task-local monotonic sequence.</param>
/// <param name="OccurredAt">The trusted Node time.</param>
/// <param name="ErrorCode">The normalized error code.</param>
/// <param name="Retryable">Whether a new idempotent dispatch may be retried.</param>
/// <param name="Payload">The bounded result or progress payload.</param>
public sealed record NodeTaskUpdate(Guid TaskId, NodeTaskState State, long Sequence, DateTimeOffset OccurredAt,
    string? ErrorCode, bool Retryable, JsonElement? Payload);
