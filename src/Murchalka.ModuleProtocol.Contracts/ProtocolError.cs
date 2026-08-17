using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes a structured invocation error.</summary>
/// <param name="Code">The machine-readable code.</param><param name="Category">The error category.</param>
/// <param name="Retryable">Whether retrying may succeed.</param><param name="Message">The human-readable message.</param>
/// <param name="Details">Optional structured details.</param>
public sealed record ProtocolError(string Code, ErrorCategory Category, bool Retryable, string Message, JsonElement? Details);
