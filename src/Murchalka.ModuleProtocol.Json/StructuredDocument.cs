using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;
using Murchalka.ModuleProtocol.Contracts;
using YamlDotNet.Serialization;

namespace Murchalka.ModuleProtocol.Json;

/// <summary>Loads JSON and YAML documents into a normalized JSON node model.</summary>
public static class StructuredDocument
{
    /// <summary>Loads a JSON or YAML document from disk.</summary>
    public static JsonNode Load(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        var file = new FileInfo(path);
        if (!file.Exists) throw new FileNotFoundException("Structured document was not found.", path);
        if (file.Length > ProtocolConstants.MaximumJsonFrameBytes)
            throw new InvalidDataException($"Structured document exceeds {ProtocolConstants.MaximumJsonFrameBytes} bytes.");
        var content = File.ReadAllText(path);
        if (Path.GetExtension(path).Equals(".json", StringComparison.OrdinalIgnoreCase))
            return JsonNode.Parse(content) ?? throw new JsonException($"'{path}' contains no JSON value.");
        var value = new DeserializerBuilder().WithAttemptingUnquotedStringTypeDeserialization()
            .WithDuplicateKeyChecking().Build().Deserialize<object?>(content);
        return JsonSerializer.SerializeToNode(NormalizeYaml(value)) ?? new JsonObject();
    }

    private static object? NormalizeYaml(object? value) => value switch
    {
        IDictionary dictionary => NormalizeDictionary(dictionary),
        IEnumerable sequence when value is not string => sequence.Cast<object?>().Select(NormalizeYaml).ToArray(),
        _ => value
    };

    private static IReadOnlyDictionary<string, object?> NormalizeDictionary(IDictionary dictionary)
    {
        var normalized = new Dictionary<string, object?>(StringComparer.Ordinal);
        foreach (DictionaryEntry entry in dictionary)
        {
            var key = entry.Key?.ToString() ?? throw new InvalidDataException("YAML mapping key cannot be null.");
            if (!normalized.TryAdd(key, NormalizeYaml(entry.Value)))
                throw new InvalidDataException($"YAML mapping contains duplicate key '{key}'.");
        }
        return normalized;
    }
}
