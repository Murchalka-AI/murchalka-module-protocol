using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Requests a module control operation.</summary>
/// <param name="OperationId">The operation identifier.</param><param name="Kind">The operation kind.</param>
/// <param name="Deadline">The operation deadline.</param><param name="Payload">The operation payload.</param>
public sealed record ControlMessage(string OperationId, ControlMessageKind Kind, DateTimeOffset Deadline, JsonElement Payload);
