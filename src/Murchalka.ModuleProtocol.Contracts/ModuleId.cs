using System.Text.RegularExpressions;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies a module using a lowercase reverse-DNS name.</summary>
public readonly record struct ModuleId
{
    private static readonly Regex Pattern = new(
        "^[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?(?:\\.[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?)+$",
        RegexOptions.CultureInvariant | RegexOptions.NonBacktracking);

    /// <summary>Initializes a module identifier.</summary>
    /// <param name="value">The lowercase reverse-DNS identifier.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is invalid.</exception>
    public ModuleId(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value.Length > 253 || !Pattern.IsMatch(value))
        {
            throw new ArgumentException("Module id must be a lowercase reverse-DNS identifier.", nameof(value));
        }

        Value = value;
    }

    /// <summary>Gets the identifier value.</summary>
    public string Value { get; }

    /// <inheritdoc/>
    public override string ToString() => Value ?? string.Empty;
}
