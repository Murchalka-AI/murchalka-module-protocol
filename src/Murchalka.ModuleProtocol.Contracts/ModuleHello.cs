namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Introduces a module instance to the runtime.</summary>
/// <param name="ModuleId">The module identifier.</param><param name="ModuleVersion">The module version.</param>
/// <param name="BundleDigest">The bundle digest.</param><param name="InstanceId">The instance identifier.</param>
/// <param name="ProtocolVersions">The offered protocol versions.</param><param name="ArtifactId">The artifact identifier.</param>
/// <param name="Target">The target runtime tier.</param><param name="ProcessIdentity">The process identity.</param>
/// <param name="DeclaredCapabilitiesDigest">The declared-capabilities digest.</param><param name="Nonce">The module nonce.</param>
public sealed record ModuleHello(ModuleId ModuleId, SemanticVersion ModuleVersion, string BundleDigest,
    InstanceId InstanceId, IReadOnlyList<int> ProtocolVersions, string ArtifactId, ModuleTarget Target,
    string ProcessIdentity, string DeclaredCapabilitiesDigest, string Nonce);
