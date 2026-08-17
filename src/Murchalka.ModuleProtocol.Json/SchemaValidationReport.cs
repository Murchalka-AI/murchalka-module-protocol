namespace Murchalka.ModuleProtocol.Json;

/// <summary>Contains the result of JSON Schema validation.</summary>
/// <param name="IsValid">Whether validation succeeded.</param><param name="Violations">The detected violations.</param>
public sealed record SchemaValidationReport(bool IsValid, IReadOnlyList<SchemaViolation> Violations)
{
    /// <summary>Gets a successful validation report.</summary>
    public static SchemaValidationReport Valid { get; } = new(true, []);
}
