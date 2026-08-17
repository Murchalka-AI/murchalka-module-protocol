namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Provides protocol and semantic-version compatibility checks.</summary>
public static class CompatibilityPolicy
{
    /// <summary>Determines whether an API version matches a supported group and major.</summary>
    public static bool SupportsApiVersion(string apiVersion, string group, int supportedMajor = 1)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiVersion);
        ArgumentException.ThrowIfNullOrWhiteSpace(group);
        return string.Equals(apiVersion, $"{group}/v{supportedMajor}", StringComparison.Ordinal);
    }

    /// <summary>Determines whether a version bump is sufficient for a contract change.</summary>
    public static bool IsVersionBumpSufficient(SemanticVersion previous, SemanticVersion next, ContractChangeKind change)
    {
        if (next <= previous) return false;
        return change switch
        {
            ContractChangeKind.Patch => true,
            ContractChangeKind.Additive => next.Major > previous.Major || next.Minor > previous.Minor,
            ContractChangeKind.Breaking => next.Major > previous.Major,
            _ => false
        };
    }

    /// <summary>Selects the greatest mutually supported protocol major.</summary>
    public static int NegotiateProtocol(IReadOnlyCollection<int> offered, IReadOnlyCollection<int>? supported = null)
    {
        ArgumentNullException.ThrowIfNull(offered);
        supported ??= ProtocolConstants.SupportedMajors;
        var selected = offered.Intersect(supported).DefaultIfEmpty(0).Max();
        if (selected == 0)
            throw new ProtocolNegotiationException("protocol-version-unsupported", "No mutually supported protocol major exists.");
        return selected;
    }
}
