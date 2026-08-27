namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Defines the hard resource budget of a Node task.</summary>
/// <param name="CpuMillis">The maximum CPU time in milliseconds.</param>
/// <param name="MemoryBytes">The maximum working-set size.</param>
/// <param name="OutputBytes">The maximum result size.</param>
public sealed record NodeResourceBudget(long CpuMillis, long MemoryBytes, int OutputBytes);
