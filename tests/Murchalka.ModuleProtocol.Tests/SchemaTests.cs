using System.Text.Json.Nodes;
using Murchalka.ModuleProtocol.Json;

namespace Murchalka.ModuleProtocol.Tests;

public sealed class SchemaTests
{
    public static TheoryData<string, string> ValidDocuments => new()
    {
        { "module-manifest.schema.json", "valid-module.yaml" },
        { "capability.schema.json", "valid-capability.json" },
        { "binding.schema.json", "valid-bindings.yaml" },
        { "profile.schema.json", "valid-profile.yaml" },
        { "permission-grant.schema.json", "valid-grant.yaml" },
        { "module-lock.schema.json", "valid-lock.json" },
        { "event-envelope.schema.json", "valid-event.json" },
        { "node-descriptor.schema.json", "valid-node-descriptor.json" },
        { "node-task.schema.json", "valid-node-task.json" },
        { "node-stream-message.schema.json", "valid-node-stream-message.json" },
        { "client-extension.schema.json", "valid-client-extension.json" },
        { "client-extension-catalog.schema.json", "valid-client-catalog.json" },
        { "client-action-request.schema.json", "valid-client-action.json" },
        { "agent-ui.schema.json", "valid-agent-ui.json" }
    };

    [Theory]
    [MemberData(nameof(ValidDocuments))]
    public void Canonical_fixture_is_valid(string schema, string fixture)
    {
        var report = Validator().ValidateFile(schema, Fixture(fixture));
        Assert.True(report.IsValid, string.Join(Environment.NewLine, report.Violations));
    }

    [Fact]
    public void Known_major_rejects_unknown_fields()
    {
        var manifest = StructuredDocument.Load(Fixture("valid-module.yaml")).AsObject();
        manifest["surpriseAuthority"] = true;

        var report = Validator().ValidateJson("module-manifest.schema.json", manifest);

        Assert.False(report.IsValid);
    }

    [Fact]
    public void Requirement_cannot_select_capability_and_category_together()
    {
        var manifest = StructuredDocument.Load(Fixture("valid-module.yaml")).AsObject();
        var optional = manifest["optional"]!["capabilities"]!.AsArray()[0]!.AsObject();
        optional["category"] = "audit.provider";

        var report = Validator().ValidateJson("module-manifest.schema.json", manifest);

        Assert.False(report.IsValid);
    }

    [Fact]
    public void Plain_http_is_allowed_only_for_explicit_loopback_targets()
    {
        var manifest = StructuredDocument.Load(Fixture("valid-module.yaml")).AsObject();
        var outbound = manifest["permissions"]!["network"]!["outbound"]!.AsArray();
        outbound.Add(JsonNode.Parse("""{"scheme":"http","host":"127.0.0.1","ports":[11434]}"""));
        Assert.True(Validator().ValidateJson("module-manifest.schema.json", manifest).IsValid);

        outbound[0]!["host"] = "ollama.example.com";

        Assert.False(Validator().ValidateJson("module-manifest.schema.json", manifest).IsValid);
    }

    private static CanonicalSchemaValidator Validator() => new(Path.Combine(AppContext.BaseDirectory, "schemas"));
    private static string Fixture(string name) => Path.Combine(AppContext.BaseDirectory, "fixtures", name);
}
