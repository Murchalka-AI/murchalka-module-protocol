namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Returns a rotated short-lived Node client certificate.</summary>
/// <param name="CertificatePem">The rotated client certificate.</param>
/// <param name="ExpiresAt">The certificate expiry.</param>
public sealed record NodeCertificateRenewalResponse(string CertificatePem, DateTimeOffset ExpiresAt);
