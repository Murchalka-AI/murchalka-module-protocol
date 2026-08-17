namespace Murchalka.ModuleProtocol.Transport;

/// <summary>Contains a typed protocol payload.</summary>
/// <param name="MessageType">The logical message type.</param><param name="Payload">The serialized payload.</param>
public sealed record ProtocolFrame(string MessageType, ReadOnlyMemory<byte> Payload)
{
    /// <summary>Gets the payload size in bytes.</summary>
    public int Size => Payload.Length;
}
