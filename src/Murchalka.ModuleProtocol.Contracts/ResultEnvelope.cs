using System.Text.Json;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains the result of a capability invocation.</summary>
/// <param name="InvocationId">The invocation identifier.</param><param name="Status">The result status.</param>
/// <param name="Payload">The result payload.</param><param name="Error">The structured error.</param>
/// <param name="Usage">The usage information.</param><param name="EvidenceReferences">Evidence references.</param>
/// <param name="ArtifactReferences">Artifact references.</param><param name="ProviderReceipt">The provider receipt.</param>
public sealed record ResultEnvelope(Guid InvocationId, InvocationStatus Status, JsonElement? Payload, ProtocolError? Error,
    UsageRecord? Usage, IReadOnlyList<Uri> EvidenceReferences, IReadOnlyList<Uri> ArtifactReferences, string? ProviderReceipt);
