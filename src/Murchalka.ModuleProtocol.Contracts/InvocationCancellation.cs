namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Requests cancellation of an in-flight invocation.</summary>
/// <param name="InvocationId">The invocation identifier.</param>
/// <param name="Reason">The stable machine-readable cancellation reason.</param>
public sealed record InvocationCancellation(Guid InvocationId, string Reason);
