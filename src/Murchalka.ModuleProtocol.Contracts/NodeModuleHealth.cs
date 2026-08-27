namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Reports Node module process readiness.</summary>
/// <param name="Ready">Whether the process can accept tasks.</param>
/// <param name="ObservedAt">The observation time.</param>
/// <param name="ReasonCode">The normalized reason when not ready.</param>
public sealed record NodeModuleHealth(bool Ready, DateTimeOffset ObservedAt, string? ReasonCode);
