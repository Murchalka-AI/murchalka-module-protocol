using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace Murchalka.ModuleProtocol.Transport;

internal sealed class InMemoryProtocolTransportEndpoint(ChannelWriter<ProtocolFrame> writer,
    ChannelReader<ProtocolFrame> reader, int maximumFrameBytes) : IProtocolTransport
{
    private int _disposed;

    public async ValueTask SendAsync(ProtocolFrame frame, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
        ArgumentNullException.ThrowIfNull(frame);
        if (frame.Size > maximumFrameBytes) throw new ProtocolFrameTooLargeException(frame.Size, maximumFrameBytes);
        await writer.WriteAsync(frame, cancellationToken).ConfigureAwait(false);
    }

    public async IAsyncEnumerable<ProtocolFrame> ReceiveAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await foreach (var frame in reader.ReadAllAsync(cancellationToken).ConfigureAwait(false))
        {
            if (frame.Size > maximumFrameBytes) throw new ProtocolFrameTooLargeException(frame.Size, maximumFrameBytes);
            yield return frame;
        }
    }

    public ValueTask DisposeAsync()
    {
        if (Interlocked.Exchange(ref _disposed, 1) == 0) writer.TryComplete();
        return ValueTask.CompletedTask;
    }
}
