namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies an authentication scheme declared by a protocol contribution.</summary>
public enum ProtocolAuthenticationScheme
{
    /// <summary>No peer credential is accepted. This mode is restricted to explicitly trusted local transports.</summary>
    None,

    /// <summary>Bearer-token authentication.</summary>
    Bearer,

    /// <summary>OAuth 2.0 authentication.</summary>
    OAuth2,

    /// <summary>Mutual TLS authentication.</summary>
    MutualTls
}
