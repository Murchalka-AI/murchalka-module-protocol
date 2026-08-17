using System.Text.Json;
using System.Text.Json.Nodes;
using Json.Schema;

namespace Murchalka.ModuleProtocol.Json;

/// <summary>Validates documents against canonical protocol schemas.</summary>
public sealed class CanonicalSchemaValidator
{
    private readonly string? _schemaDirectory;
    private readonly IReadOnlyDictionary<string, string>? _bundledSchemas;

    /// <summary>Initializes a validator backed by a schema directory.</summary>
    public CanonicalSchemaValidator(string schemaDirectory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(schemaDirectory);
        _schemaDirectory = Path.GetFullPath(schemaDirectory);
        if (!Directory.Exists(_schemaDirectory))
            throw new DirectoryNotFoundException($"Schema directory '{_schemaDirectory}' was not found.");
    }

    private CanonicalSchemaValidator(IReadOnlyDictionary<string, string> bundledSchemas) => _bundledSchemas = bundledSchemas;

    /// <summary>Creates a validator backed by schemas embedded in the assembly.</summary>
    public static CanonicalSchemaValidator CreateBundled()
    {
        const string prefix = "Murchalka.ModuleProtocol.Schemas.";
        var assembly = typeof(CanonicalSchemaValidator).Assembly;
        var schemas = assembly.GetManifestResourceNames()
            .Where(name => name.StartsWith(prefix, StringComparison.Ordinal) && name.EndsWith(".schema.json", StringComparison.Ordinal))
            .ToDictionary(name => name[prefix.Length..], name =>
            {
                using var stream = assembly.GetManifestResourceStream(name)
                    ?? throw new InvalidOperationException($"Embedded schema '{name}' cannot be opened.");
                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }, StringComparer.Ordinal);
        if (schemas.Count == 0) throw new InvalidOperationException("No canonical schemas are embedded in the protocol assembly.");
        return new CanonicalSchemaValidator(schemas);
    }

    /// <summary>Validates a JSON value against a named canonical schema.</summary>
    public SchemaValidationReport ValidateJson(string schemaFileName, JsonNode instance)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(schemaFileName);
        ArgumentNullException.ThrowIfNull(instance);
        if (Path.GetFileName(schemaFileName) != schemaFileName)
            throw new ArgumentException("Schema file name cannot contain a path.", nameof(schemaFileName));
        var schemaText = _bundledSchemas is not null
            ? _bundledSchemas.TryGetValue(schemaFileName, out var bundled) ? bundled
                : throw new FileNotFoundException($"Bundled schema '{schemaFileName}' was not found.", schemaFileName)
            : File.ReadAllText(Path.Combine(_schemaDirectory!, schemaFileName));
        var schema = JsonSchema.FromText(schemaText, new BuildOptions { SchemaRegistry = new SchemaRegistry() });
        var element = JsonSerializer.SerializeToElement(instance);
        var result = schema.Evaluate(element, new EvaluationOptions { OutputFormat = OutputFormat.List, RequireFormatValidation = true });
        if (result.IsValid) return SchemaValidationReport.Valid;
        var violations = (result.Details ?? []).Where(detail => !detail.IsValid && detail.Errors is { Count: > 0 })
            .SelectMany(detail => detail.Errors!.Select(error => new SchemaViolation(
                detail.InstanceLocation.ToString(), error.Key, error.Value)))
            .Distinct().ToArray();
        return new SchemaValidationReport(false, violations);
    }

    /// <summary>Loads and validates a structured document against a named canonical schema.</summary>
    public SchemaValidationReport ValidateFile(string schemaFileName, string documentPath) =>
        ValidateJson(schemaFileName, StructuredDocument.Load(documentPath));
}
