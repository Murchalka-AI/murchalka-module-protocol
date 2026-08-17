using System.Security.Cryptography;
using System.Text;
using Murchalka.ModuleProtocol.Contracts;

namespace Murchalka.ModuleProtocol.Client;

/// <summary>Creates HMAC-SHA-256 module authentication proofs.</summary>
public sealed class HmacModuleProofProvider : IModuleProofProvider
{
    private readonly byte[] _key;

    /// <summary>Initializes the provider with a secret key.</summary>
    public HmacModuleProofProvider(ReadOnlyMemory<byte> key)
    {
        if (key.IsEmpty) throw new ArgumentException("Proof key cannot be empty.", nameof(key));
        _key = key.ToArray();
    }

    /// <inheritdoc/>
    public string CreateProof(ModuleHello hello, RuntimeChallenge challenge)
    {
        var transcript = ModuleProofTranscript.Create(hello, challenge);
        return Convert.ToBase64String(HMACSHA256.HashData(_key, Encoding.UTF8.GetBytes(transcript)));
    }
}
