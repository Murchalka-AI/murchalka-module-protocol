namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes bounded resources advertised by a Node.</summary>
/// <param name="ProcessorCount">The available logical processor count.</param>
/// <param name="MemoryBytes">The available memory in bytes.</param>
public sealed record NodeResources(int ProcessorCount, long MemoryBytes);
