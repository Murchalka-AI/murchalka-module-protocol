namespace Murchalka.ModuleProtocol.Transport;

/// <summary>Defines an asynchronous bidirectional protocol transport.</summary>
public interface IProtocolTransport : IAsyncDisposable
{
    /// <summary>Sends a protocol frame.</summary>
    ValueTask SendAsync(ProtocolFrame frame, CancellationToken cancellationToken = default);
    /// <summary>Receives protocol frames until the transport completes.</summary>
    IAsyncEnumerable<ProtocolFrame> ReceiveAsync(CancellationToken cancellationToken = default);
}
