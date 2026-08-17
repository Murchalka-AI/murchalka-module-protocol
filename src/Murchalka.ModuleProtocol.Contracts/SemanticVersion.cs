using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Represents a Semantic Versioning 2.0.0 version.</summary>
public readonly record struct SemanticVersion : IComparable<SemanticVersion>
{
    private static readonly Regex Pattern = new(
        "^(0|[1-9]\\d*)\\.(0|[1-9]\\d*)\\.(0|[1-9]\\d*)(?:-([0-9A-Za-z-]+(?:\\.[0-9A-Za-z-]+)*))?(?:\\+([0-9A-Za-z-]+(?:\\.[0-9A-Za-z-]+)*))?$",
        RegexOptions.CultureInvariant | RegexOptions.NonBacktracking);

    /// <summary>Initializes a semantic version.</summary>
    /// <param name="major">The major version.</param>
    /// <param name="minor">The minor version.</param>
    /// <param name="patch">The patch version.</param>
    /// <param name="prerelease">The optional prerelease identifier.</param>
    /// <param name="build">The optional build metadata.</param>
    public SemanticVersion(int major, int minor, int patch, string? prerelease = null, string? build = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(major);
        ArgumentOutOfRangeException.ThrowIfNegative(minor);
        ArgumentOutOfRangeException.ThrowIfNegative(patch);
        ValidateIdentifiers(prerelease, true, nameof(prerelease));
        ValidateIdentifiers(build, false, nameof(build));
        Major = major;
        Minor = minor;
        Patch = patch;
        Prerelease = prerelease;
        Build = build;
    }

    /// <summary>Gets the major version.</summary>
    public int Major { get; }
    /// <summary>Gets the minor version.</summary>
    public int Minor { get; }
    /// <summary>Gets the patch version.</summary>
    public int Patch { get; }
    /// <summary>Gets the prerelease identifier.</summary>
    public string? Prerelease { get; }
    /// <summary>Gets the build metadata.</summary>
    public string? Build { get; }
    /// <summary>Gets whether the version is a prerelease.</summary>
    public bool IsPrerelease => Prerelease is not null;

    /// <summary>Parses a Semantic Versioning 2.0.0 value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <returns>The parsed version.</returns>
    /// <exception cref="FormatException">Thrown when the value is invalid.</exception>
    public static SemanticVersion Parse(string value)
    {
        if (!TryParse(value, out var version)) throw new FormatException($"'{value}' is not a valid SemVer 2.0.0 version.");
        return version;
    }

    /// <summary>Attempts to parse a Semantic Versioning 2.0.0 value.</summary>
    /// <param name="value">The value to parse.</param>
    /// <param name="version">Receives the parsed version.</param>
    /// <returns><see langword="true"/> when parsing succeeds.</returns>
    public static bool TryParse(string? value, out SemanticVersion version)
    {
        version = default;
        if (value is null) return false;
        var match = Pattern.Match(value);
        if (!match.Success ||
            !int.TryParse(match.Groups[1].Value, NumberStyles.None, CultureInfo.InvariantCulture, out var major) ||
            !int.TryParse(match.Groups[2].Value, NumberStyles.None, CultureInfo.InvariantCulture, out var minor) ||
            !int.TryParse(match.Groups[3].Value, NumberStyles.None, CultureInfo.InvariantCulture, out var patch) ||
            !IdentifiersAreValid(match.Groups[4].Success ? match.Groups[4].Value : null, true)) return false;

        version = new SemanticVersion(major, minor, patch,
            match.Groups[4].Success ? match.Groups[4].Value : null,
            match.Groups[5].Success ? match.Groups[5].Value : null);
        return true;
    }

    /// <inheritdoc/>
    public int CompareTo(SemanticVersion other)
    {
        var core = Major.CompareTo(other.Major);
        if (core == 0) core = Minor.CompareTo(other.Minor);
        if (core == 0) core = Patch.CompareTo(other.Patch);
        if (core != 0) return core;
        if (Prerelease is null) return other.Prerelease is null ? 0 : 1;
        if (other.Prerelease is null) return -1;
        var left = Prerelease.Split('.');
        var right = other.Prerelease.Split('.');
        for (var index = 0; index < Math.Min(left.Length, right.Length); index++)
        {
            var leftNumeric = int.TryParse(left[index], NumberStyles.None, CultureInfo.InvariantCulture, out var leftNumber);
            var rightNumeric = int.TryParse(right[index], NumberStyles.None, CultureInfo.InvariantCulture, out var rightNumber);
            var comparison = leftNumeric && rightNumeric ? leftNumber.CompareTo(rightNumber)
                : leftNumeric ? -1 : rightNumeric ? 1 : string.CompareOrdinal(left[index], right[index]);
            if (comparison != 0) return comparison;
        }
        return left.Length.CompareTo(right.Length);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var result = new StringBuilder(FormattableString.Invariant($"{Major}.{Minor}.{Patch}"));
        if (Prerelease is not null) result.Append('-').Append(Prerelease);
        if (Build is not null) result.Append('+').Append(Build);
        return result.ToString();
    }

    /// <summary>Compares two versions.</summary>
    public static bool operator <(SemanticVersion left, SemanticVersion right) => left.CompareTo(right) < 0;
    /// <summary>Compares two versions.</summary>
    public static bool operator >(SemanticVersion left, SemanticVersion right) => left.CompareTo(right) > 0;
    /// <summary>Compares two versions.</summary>
    public static bool operator <=(SemanticVersion left, SemanticVersion right) => left.CompareTo(right) <= 0;
    /// <summary>Compares two versions.</summary>
    public static bool operator >=(SemanticVersion left, SemanticVersion right) => left.CompareTo(right) >= 0;

    private static void ValidateIdentifiers(string? value, bool forbidLeadingZeroes, string parameterName)
    {
        if (!IdentifiersAreValid(value, forbidLeadingZeroes)) throw new ArgumentException("SemVer identifier is invalid.", parameterName);
    }

    private static bool IdentifiersAreValid(string? value, bool forbidLeadingZeroes)
    {
        if (value is null) return true;
        if (value.Length == 0) return false;
        foreach (var identifier in value.Split('.'))
        {
            if (identifier.Length == 0 || identifier.Any(character => !char.IsAsciiLetterOrDigit(character) && character != '-')) return false;
            if (forbidLeadingZeroes && identifier.Length > 1 && identifier[0] == '0' && identifier.All(char.IsAsciiDigit)) return false;
        }
        return true;
    }
}
