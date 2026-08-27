namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes a Node operating system and architecture.</summary>
/// <param name="OperatingSystem">The normalized operating system name.</param>
/// <param name="Version">The operating system version.</param>
/// <param name="Architecture">The normalized process architecture.</param>
public sealed record NodePlatform(string OperatingSystem, string Version, string Architecture);
