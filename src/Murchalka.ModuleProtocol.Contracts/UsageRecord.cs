namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Reports resource usage for an invocation.</summary>
/// <param name="InputBytes">The number of input bytes.</param><param name="OutputBytes">The number of output bytes.</param>
/// <param name="DurationMilliseconds">The duration in milliseconds.</param><param name="Cost">The optional monetary cost.</param>
public sealed record UsageRecord(long? InputBytes, long? OutputBytes, long? DurationMilliseconds, decimal? Cost);
