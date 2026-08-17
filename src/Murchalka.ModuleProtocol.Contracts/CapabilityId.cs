using System.Text.RegularExpressions;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies a capability using a lowercase dotted name.</summary>
public readonly record struct CapabilityId
{
    private static readonly Regex Pattern = new(
        "^[a-z][a-z0-9]*(?:[.-][a-z][a-z0-9-]*)+$",
        RegexOptions.CultureInvariant | RegexOptions.NonBacktracking);

    /// <summary>Initializes a capability identifier.</summary>
    /// <param name="value">The lowercase dotted identifier.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is invalid.</exception>
    public CapabilityId(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value.Length > 160 || !Pattern.IsMatch(value))
        {
            throw new ArgumentException("Capability id must be a lowercase dotted identifier.", nameof(value));
        }

        Value = value;
    }

    /// <summary>Gets the identifier value.</summary>
    public string Value { get; }

    /// <inheritdoc/>
    public override string ToString() => Value ?? string.Empty;
}
