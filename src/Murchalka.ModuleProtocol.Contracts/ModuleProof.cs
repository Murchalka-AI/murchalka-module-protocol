namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains a module authentication proof.</summary>
/// <param name="ModuleId">The module identifier.</param><param name="InstanceId">The instance identifier.</param>
/// <param name="RuntimeNonce">The runtime nonce.</param><param name="ModuleNonce">The module nonce.</param>
/// <param name="Proof">The encoded proof.</param>
public sealed record ModuleProof(ModuleId ModuleId, InstanceId InstanceId, string RuntimeNonce, string ModuleNonce, string Proof);
