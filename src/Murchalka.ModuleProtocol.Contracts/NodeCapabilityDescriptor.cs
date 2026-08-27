namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes a capability provided by a Node module instance.</summary>
/// <param name="CapabilityId">The capability identifier.</param>
/// <param name="Version">The capability version.</param>
/// <param name="Category">The capability category.</param>
/// <param name="ProviderInstance">The authenticated provider instance.</param>
/// <param name="ModuleId">The providing module identifier.</param>
/// <param name="ModuleVersion">The providing module version.</param>
/// <param name="ContractDigest">The capability contract digest.</param>
public sealed record NodeCapabilityDescriptor(CapabilityId CapabilityId, SemanticVersion Version, string Category,
    InstanceId ProviderInstance, ModuleId ModuleId, SemanticVersion ModuleVersion, string ContractDigest);
