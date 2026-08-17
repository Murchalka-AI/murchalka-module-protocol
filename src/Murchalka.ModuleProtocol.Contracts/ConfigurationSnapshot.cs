using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains an immutable configuration revision.</summary>
/// <param name="Revision">The revision number.</param><param name="SchemaDigest">The configuration schema digest.</param>
/// <param name="Values">The configuration values.</param>
public sealed record ConfigurationSnapshot(long Revision, string SchemaDigest, JsonElement Values);
