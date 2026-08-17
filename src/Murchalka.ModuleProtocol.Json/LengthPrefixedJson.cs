using System.Buffers;
using Murchalka.ModuleProtocol.Contracts;

namespace Murchalka.ModuleProtocol.Json;

/// <summary>Reads and writes length-prefixed JSON protocol frames.</summary>
public static class LengthPrefixedJson
{
    /// <summary>Writes a length-prefixed JSON message.</summary>
    public static async ValueTask WriteAsync<T>(Stream stream, T message,
        int maximumFrameBytes = ProtocolConstants.MaximumJsonFrameBytes, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stream);
        var payload = ProtocolJson.Serialize(message);
        if (payload.Length > maximumFrameBytes)
            throw new InvalidDataException($"JSON frame is {payload.Length} bytes; maximum is {maximumFrameBytes}.");
        var prefix = BitConverter.GetBytes(System.Net.IPAddress.HostToNetworkOrder(payload.Length));
        await stream.WriteAsync(prefix, cancellationToken).ConfigureAwait(false);
        await stream.WriteAsync(payload, cancellationToken).ConfigureAwait(false);
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Reads a length-prefixed JSON message.</summary>
    public static async ValueTask<T> ReadAsync<T>(Stream stream,
        int maximumFrameBytes = ProtocolConstants.MaximumJsonFrameBytes, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stream);
        var prefix = new byte[sizeof(int)];
        await ReadExactlyAsync(stream, prefix, cancellationToken).ConfigureAwait(false);
        var length = System.Net.IPAddress.NetworkToHostOrder(BitConverter.ToInt32(prefix));
        if (length < 0 || length > maximumFrameBytes)
            throw new InvalidDataException($"Invalid JSON frame length {length}; maximum is {maximumFrameBytes}.");
        var payload = ArrayPool<byte>.Shared.Rent(length);
        try
        {
            await ReadExactlyAsync(stream, payload.AsMemory(0, length), cancellationToken).ConfigureAwait(false);
            return ProtocolJson.Deserialize<T>(payload.AsSpan(0, length));
        }
        finally { ArrayPool<byte>.Shared.Return(payload, clearArray: true); }
    }

    private static async ValueTask ReadExactlyAsync(Stream stream, Memory<byte> buffer, CancellationToken cancellationToken)
    {
        var offset = 0;
        while (offset < buffer.Length)
        {
            var count = await stream.ReadAsync(buffer[offset..], cancellationToken).ConfigureAwait(false);
            if (count == 0) throw new EndOfStreamException("Protocol frame ended before its declared length.");
            offset += count;
        }
    }
}
