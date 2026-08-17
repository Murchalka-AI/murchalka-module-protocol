using System.Threading.Channels;

namespace Murchalka.ModuleProtocol.Transport;

/// <summary>Creates connected in-memory protocol transports.</summary>
public static class InMemoryProtocolTransport
{
    /// <summary>Creates a bounded pair of connected runtime and module endpoints.</summary>
    public static (IProtocolTransport Runtime, IProtocolTransport Module) CreatePair(
        int capacity = 64, int maximumFrameBytes = 4 * 1024 * 1024)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(maximumFrameBytes, 1);
        var runtimeToModule = Channel.CreateBounded<ProtocolFrame>(new BoundedChannelOptions(capacity)
        { FullMode = BoundedChannelFullMode.Wait, SingleReader = true, SingleWriter = false });
        var moduleToRuntime = Channel.CreateBounded<ProtocolFrame>(new BoundedChannelOptions(capacity)
        { FullMode = BoundedChannelFullMode.Wait, SingleReader = true, SingleWriter = false });
        return (
            new InMemoryProtocolTransportEndpoint(runtimeToModule.Writer, moduleToRuntime.Reader, maximumFrameBytes),
            new InMemoryProtocolTransportEndpoint(moduleToRuntime.Writer, runtimeToModule.Reader, maximumFrameBytes));
    }
}
