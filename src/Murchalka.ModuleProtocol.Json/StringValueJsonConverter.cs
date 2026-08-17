using System.Text.Json;
using System.Text.Json.Serialization;

namespace Murchalka.ModuleProtocol.Json;

internal sealed class StringValueJsonConverter<T>(Func<string, T> parse, Func<T, string> format) : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String) throw new JsonException($"Expected a string for {typeof(T).Name}.");
        var value = reader.GetString() ?? throw new JsonException("String value cannot be null.");
        try { return parse(value); }
        catch (Exception exception) when (exception is ArgumentException or FormatException)
        {
            throw new JsonException(exception.Message, exception);
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
        writer.WriteStringValue(format(value));
}
