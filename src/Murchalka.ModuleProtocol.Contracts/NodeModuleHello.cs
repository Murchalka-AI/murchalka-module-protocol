namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Declares the identity of a newly started Node module process.</summary>
/// <param name="ModuleId">The module identifier.</param>
/// <param name="ModuleVersion">The module version.</param>
/// <param name="BundleDigest">The verified bundle digest.</param>
/// <param name="ArtifactId">The selected artifact identifier.</param>
/// <param name="ProtocolVersion">The Node module process protocol version.</param>
/// <param name="Capabilities">The capabilities configured by the process.</param>
public sealed record NodeModuleHello(ModuleId ModuleId, SemanticVersion ModuleVersion, string BundleDigest,
    string ArtifactId, int ProtocolVersion, IReadOnlyList<NodeCapabilityDescriptor> Capabilities);
