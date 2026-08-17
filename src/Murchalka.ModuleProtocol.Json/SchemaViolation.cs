namespace Murchalka.ModuleProtocol.Json;

/// <summary>Describes a JSON Schema validation violation.</summary>
/// <param name="InstanceLocation">The instance location.</param><param name="Keyword">The failed keyword.</param>
/// <param name="Message">The validation message.</param>
public sealed record SchemaViolation(string InstanceLocation, string Keyword, string Message);
