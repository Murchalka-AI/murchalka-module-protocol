namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Reports the observed health of a module.</summary>
/// <param name="Status">The health status.</param><param name="ObservedAt">The observation time.</param>
/// <param name="ReasonCodes">Machine-readable reason codes.</param>
public sealed record ModuleHealth(ModuleHealthStatus Status, DateTimeOffset ObservedAt, IReadOnlyList<string> ReasonCodes);
