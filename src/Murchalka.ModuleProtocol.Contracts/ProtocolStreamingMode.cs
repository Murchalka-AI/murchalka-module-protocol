namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies the streaming shape exposed by a protocol contribution.</summary>
public enum ProtocolStreamingMode
{
    /// <summary>The protocol contribution uses bounded request-response messages.</summary>
    None,

    /// <summary>The protocol contribution can stream responses to the peer.</summary>
    Server,

    /// <summary>The protocol contribution can stream in both directions.</summary>
    Duplex
}
