using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains an immutable permission grant revision.</summary>
/// <param name="Revision">The revision number.</param><param name="GrantId">The grant identifier.</param>
/// <param name="BundleDigest">The bound bundle digest.</param><param name="IssuedAt">The issue time.</param>
/// <param name="ExpiresAt">The optional expiration time.</param><param name="Grant">The grant document.</param>
public sealed record PermissionGrantSnapshot(long Revision, string GrantId, string BundleDigest,
    DateTimeOffset IssuedAt, DateTimeOffset? ExpiresAt, JsonElement Grant);
