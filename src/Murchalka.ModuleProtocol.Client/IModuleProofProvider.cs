using Murchalka.ModuleProtocol.Contracts;

namespace Murchalka.ModuleProtocol.Client;

/// <summary>Creates authentication proofs for module sessions.</summary>
public interface IModuleProofProvider
{
    /// <summary>Creates a proof bound to a hello message and runtime challenge.</summary>
    string CreateProof(ModuleHello hello, RuntimeChallenge challenge);
}
