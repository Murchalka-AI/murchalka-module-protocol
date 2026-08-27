namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Wraps one fully validated Node task for an isolated module process.</summary>
/// <param name="Task">The validated task.</param>
public sealed record NodeModuleInvocation(NodeTask Task);
