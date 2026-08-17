using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Reports the result of a control operation.</summary>
/// <param name="OperationId">The operation identifier.</param><param name="Succeeded">Whether the operation succeeded.</param>
/// <param name="ErrorCode">The optional error code.</param><param name="ErrorMessage">The optional error message.</param>
/// <param name="Details">Optional result details.</param>
public sealed record ControlResult(string OperationId, bool Succeeded, string? ErrorCode, string? ErrorMessage, JsonElement? Details);
