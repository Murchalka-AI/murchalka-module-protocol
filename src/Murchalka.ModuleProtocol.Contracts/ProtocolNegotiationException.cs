namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Represents a protocol negotiation failure.</summary>
/// <param name="code">The machine-readable failure code.</param><param name="message">The failure message.</param>
public sealed class ProtocolNegotiationException(string code, string message) : Exception(message)
{
    /// <summary>Gets the machine-readable failure code.</summary>
    public string Code { get; } = code;
}
