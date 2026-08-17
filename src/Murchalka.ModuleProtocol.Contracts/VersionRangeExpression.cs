using System.Globalization;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Represents a conjunction of semantic-version comparators.</summary>
public sealed class VersionRangeExpression
{
    private readonly IReadOnlyList<VersionComparator> _comparators;

    private VersionRangeExpression(string original, IReadOnlyList<VersionComparator> comparators, bool allowsPrerelease)
    {
        Original = original;
        _comparators = comparators;
        AllowsPrerelease = allowsPrerelease;
    }

    /// <summary>Gets the original range expression.</summary>
    public string Original { get; }
    /// <summary>Gets whether the range explicitly permits prerelease versions.</summary>
    public bool AllowsPrerelease { get; }

    /// <summary>Parses a version range expression.</summary>
    /// <param name="expression">The expression to parse.</param>
    /// <returns>The parsed expression.</returns>
    public static VersionRangeExpression Parse(string expression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        var normalized = expression.Trim();
        if (normalized == "*") return new VersionRangeExpression(normalized, [], false);
        var comparators = new List<VersionComparator>();
        var allowsPrerelease = false;
        foreach (var token in normalized.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var (op, versionText) = SplitComparator(token);
            if (op is null && TryExpandPartial(versionText, comparators)) continue;
            var version = SemanticVersion.Parse(versionText);
            allowsPrerelease |= version.IsPrerelease;
            comparators.Add(new VersionComparator(op ?? VersionComparatorOperator.Equal, version));
        }
        return new VersionRangeExpression(normalized, comparators, allowsPrerelease);
    }

    /// <summary>Determines whether a version satisfies the expression.</summary>
    /// <param name="candidate">The version to test.</param>
    /// <returns><see langword="true"/> when the version satisfies every comparator.</returns>
    public bool Satisfies(SemanticVersion candidate) =>
        (!candidate.IsPrerelease || AllowsPrerelease) && _comparators.All(comparator => comparator.Matches(candidate));

    private static (VersionComparatorOperator? Operator, string Version) SplitComparator(string token)
    {
        foreach (var item in new[]
        {
            (Text: ">=", Operator: VersionComparatorOperator.GreaterThanOrEqual),
            (Text: "<=", Operator: VersionComparatorOperator.LessThanOrEqual),
            (Text: ">", Operator: VersionComparatorOperator.GreaterThan),
            (Text: "<", Operator: VersionComparatorOperator.LessThan),
            (Text: "=", Operator: VersionComparatorOperator.Equal)
        })
        {
            if (!token.StartsWith(item.Text, StringComparison.Ordinal)) continue;
            var value = token[item.Text.Length..];
            if (value.Length == 0) throw new FormatException($"Missing version after '{item.Text}'.");
            return (item.Operator, value);
        }
        return (null, token);
    }

    private static bool TryExpandPartial(string value, ICollection<VersionComparator> comparators)
    {
        var parts = value.Split('.');
        if (parts.Length is not (1 or 2) || parts.Any(part => !int.TryParse(part, NumberStyles.None, CultureInfo.InvariantCulture, out _))) return false;
        var major = int.Parse(parts[0], CultureInfo.InvariantCulture);
        if (parts.Length == 1)
        {
            comparators.Add(new VersionComparator(VersionComparatorOperator.GreaterThanOrEqual, new SemanticVersion(major, 0, 0)));
            comparators.Add(new VersionComparator(VersionComparatorOperator.LessThan, new SemanticVersion(checked(major + 1), 0, 0)));
            return true;
        }
        var minor = int.Parse(parts[1], CultureInfo.InvariantCulture);
        comparators.Add(new VersionComparator(VersionComparatorOperator.GreaterThanOrEqual, new SemanticVersion(major, minor, 0)));
        comparators.Add(new VersionComparator(VersionComparatorOperator.LessThan, new SemanticVersion(major, checked(minor + 1), 0)));
        return true;
    }
}
