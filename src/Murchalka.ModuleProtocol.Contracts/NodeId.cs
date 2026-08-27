using System.Text.RegularExpressions;

namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies an enrolled Node.</summary>
public readonly record struct NodeId
{
    private static readonly Regex Pattern = new("^node-[a-z0-9](?:[a-z0-9-]{0,62}[a-z0-9])?$", RegexOptions.CultureInvariant | RegexOptions.NonBacktracking);

    /// <summary>Initializes a Node identifier.</summary>
    /// <param name="value">The stable lowercase Node identifier.</param>
    public NodeId(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (!Pattern.IsMatch(value)) throw new ArgumentException("Node id must start with 'node-' and contain lowercase letters, digits, or hyphens.", nameof(value));
        Value = value;
    }

    /// <summary>Gets the identifier value.</summary>
    public string Value { get; }

    /// <inheritdoc/>
    public override string ToString() => Value ?? string.Empty;
}
