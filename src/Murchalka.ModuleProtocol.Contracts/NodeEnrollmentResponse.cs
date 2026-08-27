namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Reports enrollment state and approved identity material.</summary>
/// <param name="RequestId">The enrollment request identifier.</param>
/// <param name="State">The current enrollment state.</param>
/// <param name="NodeId">The assigned Node identifier when approved.</param>
/// <param name="CertificatePem">The short-lived client certificate when approved.</param>
/// <param name="CertificateChainPem">The issuing certificate chain when approved.</param>
/// <param name="ExpiresAt">The request or certificate expiry.</param>
public sealed record NodeEnrollmentResponse(Guid RequestId, NodeEnrollmentState State, NodeId? NodeId,
    string? CertificatePem, string? CertificateChainPem, DateTimeOffset ExpiresAt);
