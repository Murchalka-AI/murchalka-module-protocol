namespace Murchalka.ModuleProtocol.Transport;

/// <summary>Indicates that a protocol frame exceeds the configured limit.</summary>
/// <param name="actualBytes">The actual frame size.</param><param name="maximumBytes">The configured maximum size.</param>
public sealed class ProtocolFrameTooLargeException(int actualBytes, int maximumBytes)
    : Exception($"Protocol frame is {actualBytes} bytes; maximum is {maximumBytes} bytes.")
{
    /// <summary>Gets the actual frame size.</summary>
    public int ActualBytes { get; } = actualBytes;
    /// <summary>Gets the configured maximum frame size.</summary>
    public int MaximumBytes { get; } = maximumBytes;
}
