namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Defines protocol-wide limits and supported versions.</summary>
public static class ProtocolConstants
{
    /// <summary>The current protocol major version.</summary>
    public const int CurrentMajor = 1;

    /// <summary>The maximum supported length of a JSON frame.</summary>
    public const int MaximumJsonFrameBytes = 4 * 1024 * 1024;

    /// <summary>The maximum payload length that can be carried inline.</summary>
    public const int MaximumInlinePayloadBytes = 256 * 1024;

    /// <summary>Gets the protocol major versions supported by this library.</summary>
    public static IReadOnlySet<int> SupportedMajors { get; } = new HashSet<int> { CurrentMajor };
}
