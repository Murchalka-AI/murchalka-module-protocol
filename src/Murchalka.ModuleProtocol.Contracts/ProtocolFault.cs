namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes a protocol session fault.</summary>
/// <param name="Code">The machine-readable code.</param><param name="Message">The human-readable message.</param>
/// <param name="Retryable">Whether retrying may succeed.</param>
public sealed record ProtocolFault(string Code, string Message, bool Retryable = false);
