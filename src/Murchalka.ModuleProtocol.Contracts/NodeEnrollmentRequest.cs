namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Requests one-time enrollment without transferring the Node private key.</summary>
/// <param name="EnrollmentToken">The one-time enrollment token.</param>
/// <param name="DisplayName">The operator-facing Node name.</param>
/// <param name="PublicKeySpki">The Base64 DER SubjectPublicKeyInfo generated on the Node.</param>
/// <param name="Platform">The requesting platform.</param>
/// <param name="Nonce">The unique request nonce.</param>
public sealed record NodeEnrollmentRequest(string EnrollmentToken, string DisplayName, string PublicKeySpki,
    NodePlatform Platform, string Nonce);
