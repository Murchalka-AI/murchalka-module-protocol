namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes an immutable Node artifact selected from a verified bundle.</summary>
/// <param name="ModuleId">The module identifier.</param>
/// <param name="ModuleVersion">The module version.</param>
/// <param name="BundleDigest">The verified bundle digest.</param>
/// <param name="ArtifactId">The selected artifact identifier.</param>
/// <param name="ArtifactDigest">The selected artifact digest.</param>
/// <param name="OperatingSystem">The selected operating system.</param>
/// <param name="Architecture">The selected architecture.</param>
public sealed record NodeArtifactDescriptor(ModuleId ModuleId, SemanticVersion ModuleVersion, string BundleDigest,
    string ArtifactId, string ArtifactDigest, string OperatingSystem, string Architecture);
