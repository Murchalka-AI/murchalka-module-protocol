namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Contains an immutable dependency binding revision.</summary>
/// <param name="BindingRevision">The binding revision.</param><param name="Endpoints">The resolved endpoints.</param>
public sealed record DependencyEndpointsSnapshot(long BindingRevision, IReadOnlyList<DependencyEndpoint> Endpoints);
