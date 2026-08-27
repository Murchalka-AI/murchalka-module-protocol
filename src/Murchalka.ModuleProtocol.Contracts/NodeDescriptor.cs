namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains the authenticated snapshot of an enrolled Node.</summary>
/// <param name="NodeId">The Node identifier.</param>
/// <param name="OwnerScope">The owning person or tenant scope.</param>
/// <param name="Platform">The Node platform.</param>
/// <param name="RuntimeVersion">The Node Runtime version.</param>
/// <param name="Labels">Administrator-controlled deployment labels.</param>
/// <param name="Resources">The available resources.</param>
/// <param name="Capabilities">The active Node capability providers.</param>
/// <param name="Attestation">The opaque attestation claims.</param>
/// <param name="LastSeenAt">The last authenticated observation time.</param>
/// <param name="Sequence">The monotonic descriptor sequence.</param>
public sealed record NodeDescriptor(NodeId NodeId, string OwnerScope, NodePlatform Platform, SemanticVersion RuntimeVersion,
    IReadOnlyDictionary<string, string> Labels, NodeResources Resources, IReadOnlyList<NodeCapabilityDescriptor> Capabilities,
    IReadOnlyDictionary<string, string> Attestation, DateTimeOffset LastSeenAt, long Sequence);
