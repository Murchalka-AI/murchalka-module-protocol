using System.Text.RegularExpressions;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies a concrete module instance.</summary>
public readonly record struct InstanceId
{
    private static readonly Regex Pattern = new(
        "^[a-zA-Z0-9][a-zA-Z0-9._:-]{0,127}$",
        RegexOptions.CultureInvariant | RegexOptions.NonBacktracking);

    /// <summary>Initializes a module instance identifier.</summary>
    /// <param name="value">The instance identifier.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is invalid.</exception>
    public InstanceId(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (!Pattern.IsMatch(value))
        {
            throw new ArgumentException("Instance id contains unsupported characters.", nameof(value));
        }

        Value = value;
    }

    /// <summary>Gets the identifier value.</summary>
    public string Value { get; }

    /// <inheritdoc/>
    public override string ToString() => Value ?? string.Empty;
}
