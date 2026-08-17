namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes a resolved capability provider endpoint.</summary>
/// <param name="RequirementId">The requirement identifier.</param><param name="ProviderModule">The provider module.</param>
/// <param name="ProviderVersion">The provider version.</param><param name="Capability">The capability identifier.</param>
/// <param name="CapabilityVersion">The capability version.</param><param name="ProviderInstance">The provider instance.</param>
/// <param name="Endpoint">The endpoint URI.</param><param name="AuthorizationReference">The authorization reference.</param>
public sealed record DependencyEndpoint(string RequirementId, ModuleId ProviderModule, SemanticVersion ProviderVersion,
    CapabilityId Capability, SemanticVersion CapabilityVersion, InstanceId ProviderInstance, Uri Endpoint,
    string AuthorizationReference);
