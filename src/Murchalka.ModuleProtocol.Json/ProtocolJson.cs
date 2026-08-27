using System.Text.Json;
using System.Text.Json.Serialization;
using Murchalka.ModuleProtocol.Contracts;

namespace Murchalka.ModuleProtocol.Json;

/// <summary>Provides canonical JSON serialization for protocol messages.</summary>
public static class ProtocolJson
{
    /// <summary>Gets the shared protocol serializer options.</summary>
    public static JsonSerializerOptions Options { get; } = CreateOptions();

    /// <summary>Serializes a protocol message to UTF-8 JSON.</summary>
    public static byte[] Serialize<T>(T message) => JsonSerializer.SerializeToUtf8Bytes(message, Options);

    /// <summary>Deserializes a protocol message from UTF-8 JSON.</summary>
    public static T Deserialize<T>(ReadOnlySpan<byte> payload)
    {
        var result = JsonSerializer.Deserialize<T>(payload, Options);
        return result ?? throw new JsonException($"The payload did not contain a {typeof(T).Name}.");
    }

    private static JsonSerializerOptions CreateOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = false,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
            WriteIndented = false
        };
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
        options.Converters.Add(new StringValueJsonConverter<ModuleId>(value => new ModuleId(value), value => value.Value));
        options.Converters.Add(new StringValueJsonConverter<CapabilityId>(value => new CapabilityId(value), value => value.Value));
        options.Converters.Add(new StringValueJsonConverter<InstanceId>(value => new InstanceId(value), value => value.Value));
        options.Converters.Add(new StringValueJsonConverter<NodeId>(value => new NodeId(value), value => value.Value));
        options.Converters.Add(new StringValueJsonConverter<SemanticVersion>(SemanticVersion.Parse, value => value.ToString()));
        return options;
    }
}
