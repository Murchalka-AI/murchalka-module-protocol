namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes a bounded route contribution supplied by an installable protocol module.</summary>
/// <param name="Id">The globally unique contribution identifier.</param>
/// <param name="Version">The contribution contract major version.</param>
/// <param name="RouteNamespace">The gateway-owned route namespace.</param>
/// <param name="HandlerCapability">The bounded capability invoked by the generic gateway.</param>
/// <param name="DescriptorPath">The bundle-relative discovery descriptor.</param>
/// <param name="Transports">The supported external transports.</param>
/// <param name="Authentication">The accepted peer authentication schemes.</param>
/// <param name="Streaming">The supported streaming shape.</param>
/// <param name="MaximumPayloadBytes">The maximum external payload size.</param>
/// <param name="MaximumConcurrency">The maximum concurrent requests.</param>
/// <param name="Timeout">The maximum request duration.</param>
public sealed record ProtocolContribution(
    string Id,
    int Version,
    string RouteNamespace,
    CapabilityId HandlerCapability,
    string DescriptorPath,
    IReadOnlySet<ProtocolTransportKind> Transports,
    IReadOnlySet<ProtocolAuthenticationScheme> Authentication,
    ProtocolStreamingMode Streaming,
    int MaximumPayloadBytes,
    int MaximumConcurrency,
    TimeSpan Timeout);
