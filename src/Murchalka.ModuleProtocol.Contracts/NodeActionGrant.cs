namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Authorizes one bounded Node action.</summary>
/// <param name="GrantId">The grant identifier.</param>
/// <param name="NodeId">The only Node allowed to execute the action.</param>
/// <param name="ConsumerModuleId">The requesting module.</param>
/// <param name="ProviderModuleId">The target Node module.</param>
/// <param name="CapabilityId">The allowed capability.</param>
/// <param name="ArgumentsDigest">The SHA-256 digest of the canonical arguments.</param>
/// <param name="IssuedAt">The grant issue time.</param>
/// <param name="ExpiresAt">The grant expiry time.</param>
/// <param name="PolicyRevision">The local/central policy revision.</param>
/// <param name="Nonce">The replay-protection nonce.</param>
/// <param name="KeyId">The signing key identifier.</param>
/// <param name="Signature">The Base64 ECDSA signature over the canonical grant.</param>
public sealed record NodeActionGrant(Guid GrantId, NodeId NodeId, ModuleId ConsumerModuleId, ModuleId ProviderModuleId,
    CapabilityId CapabilityId, string ArgumentsDigest, DateTimeOffset IssuedAt, DateTimeOffset ExpiresAt,
    long PolicyRevision, string Nonce, string KeyId, string Signature);
