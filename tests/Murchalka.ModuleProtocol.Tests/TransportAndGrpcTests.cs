using System.Text;
using Murchalka.ModuleProtocol.Transport;
using GrpcService = global::Murchalka.ModuleProtocol.Grpc.V1.ModuleProtocol;

namespace Murchalka.ModuleProtocol.Tests;

public sealed class TransportAndGrpcTests
{
    [Fact]
    public async Task In_memory_transport_is_bounded_and_bidirectional()
    {
        var (runtime, module) = InMemoryProtocolTransport.CreatePair(capacity: 1, maximumFrameBytes: 32);
        await using (runtime)
        await using (module)
        {
            await runtime.SendAsync(new ProtocolFrame("challenge", Encoding.UTF8.GetBytes("hello")));
            var received = await FirstAsync(module.ReceiveAsync());
            Assert.Equal("challenge", received.MessageType);

            await Assert.ThrowsAsync<ProtocolFrameTooLargeException>(async () =>
                await module.SendAsync(new ProtocolFrame("proof", new byte[33])));
        }
    }

    [Fact]
    public void Grpc_contract_exposes_connect_invoke_and_health()
    {
        var service = GrpcService.Descriptor;
        Assert.Equal("murchalka.moduleprotocol.v1.ModuleProtocol", service.FullName);
        Assert.Equal(["Connect", "Invoke", "ObserveHealth"], service.Methods.Select(method => method.Name));
    }

    private static async Task<ProtocolFrame> FirstAsync(IAsyncEnumerable<ProtocolFrame> frames)
    {
        await foreach (var frame in frames) return frame;
        throw new InvalidOperationException("Transport completed without a frame.");
    }
}
