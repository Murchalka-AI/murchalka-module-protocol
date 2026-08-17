namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Signals that a module instance is ready for activation.</summary>
/// <param name="ModuleId">The module identifier.</param><param name="InstanceId">The instance identifier.</param>
/// <param name="EffectiveCapabilitiesDigest">The effective capabilities digest.</param><param name="ReadyAt">The ready time.</param>
public sealed record ModuleReady(ModuleId ModuleId, InstanceId InstanceId, string EffectiveCapabilitiesDigest, DateTimeOffset ReadyAt);
