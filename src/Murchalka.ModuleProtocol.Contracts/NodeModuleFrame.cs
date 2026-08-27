using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains one bounded Node module process protocol message.</summary>
/// <param name="Kind">The message kind.</param>
/// <param name="OperationId">The correlated operation identifier.</param>
/// <param name="Payload">The kind-specific payload.</param>
public sealed record NodeModuleFrame(NodeModuleMessageKind Kind, Guid OperationId, JsonElement Payload);
