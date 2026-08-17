using Murchalka.ModuleProtocol.Contracts;

namespace Murchalka.ModuleProtocol.Client;

/// <summary>Creates the canonical transcript used by module proof algorithms.</summary>
public static class ModuleProofTranscript
{
    /// <summary>Creates a version-one proof transcript.</summary>
    public static string Create(ModuleHello hello, RuntimeChallenge challenge) => string.Join('\n',
        "murchalka-module-proof-v1", hello.ModuleId.Value, hello.ModuleVersion.ToString(), hello.BundleDigest,
        hello.InstanceId.Value, hello.ArtifactId, hello.DeclaredCapabilitiesDigest,
        challenge.SelectedProtocolVersion.ToString(System.Globalization.CultureInfo.InvariantCulture),
        challenge.ModuleNonce, challenge.RuntimeNonce);
}
