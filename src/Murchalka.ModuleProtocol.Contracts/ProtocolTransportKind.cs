namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies a transport supported by a protocol contribution.</summary>
public enum ProtocolTransportKind
{
    /// <summary>HTTP request-response transport.</summary>
    Http,

    /// <summary>HTTP Server-Sent Events transport.</summary>
    ServerSentEvents,

    /// <summary>WebSocket transport.</summary>
    WebSocket,

    /// <summary>Standard input and output transport.</summary>
    StandardIo
}
