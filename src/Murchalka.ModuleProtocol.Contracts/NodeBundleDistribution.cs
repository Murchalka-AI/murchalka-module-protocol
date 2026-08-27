namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains an immutable bundle selected for central Node deployment.</summary>
/// <param name="DeploymentId">The rollout identifier.</param>
/// <param name="BundleBase64">The Base64 encoded signed bundle.</param>
public sealed record NodeBundleDistribution(Guid DeploymentId, string BundleBase64);
