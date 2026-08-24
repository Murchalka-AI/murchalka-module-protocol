using System.Text;
using System.Text.Json;
using Murchalka.ModuleProtocol.Client;
using Murchalka.ModuleProtocol.Contracts;
using Murchalka.ModuleProtocol.Json;

namespace Murchalka.ModuleProtocol.Tests;

public sealed class ProtocolSessionTests
{
    private static readonly DateTimeOffset Now = new(2026, 8, 17, 10, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Full_handshake_reaches_active_only_after_all_snapshots()
    {
        var session = new ModuleProtocolSession(Hello(), new HmacModuleProofProvider(Encoding.UTF8.GetBytes("conformance-secret")));
        var hello = session.CreateHello();
        session.AcceptChallenge(Challenge(hello), Now);
        var proof = session.CreateProof();

        Assert.NotEmpty(proof.Proof);
        session.ApplyConfiguration(new ConfigurationSnapshot(1, Digest('c'), JsonSerializer.SerializeToElement(new { })));
        session.ApplyGrant(new PermissionGrantSnapshot(1, "grant-01", hello.BundleDigest, Now, null, JsonSerializer.SerializeToElement(new { })), Now);
        session.ApplyDependencies(new DependencyEndpointsSnapshot(1, []));
        session.CreateReady(Now);
        session.AcceptControl(Control(ControlMessageKind.Activate), Now);

        Assert.Equal(ModuleSessionState.Active, session.State);
    }

    [Fact]
    public void Activate_before_ready_faults_the_session()
    {
        var session = new ModuleProtocolSession(Hello(), new HmacModuleProofProvider(new byte[] { 1, 2, 3 }));
        session.CreateHello();

        Assert.Throws<InvalidOperationException>(() => session.AcceptControl(Control(ControlMessageKind.Activate), Now));
        Assert.Equal(ModuleSessionState.Faulted, session.State);
    }

    [Fact]
    public void Json_round_trip_preserves_strong_identifiers()
    {
        var hello = Hello();
        var payload = ProtocolJson.Serialize(hello);

        var restored = ProtocolJson.Deserialize<ModuleHello>(payload);

        Assert.Equal(hello.ProtocolVersions, restored.ProtocolVersions);
        Assert.Equal(hello with { ProtocolVersions = restored.ProtocolVersions }, restored);
    }

    [Fact]
    public async Task Length_prefix_rejects_oversized_payload_before_writing()
    {
        await using var stream = new MemoryStream();
        await Assert.ThrowsAsync<InvalidDataException>(async () =>
            await LengthPrefixedJson.WriteAsync(
                stream,
                new { Value = new string('x', 100) },
                maximumFrameBytes: 10,
                cancellationToken: TestContext.Current.CancellationToken));
        Assert.Equal(0, stream.Length);
    }

    private static ModuleHello Hello() => new(
        new ModuleId("dev.murchalka.hello"), SemanticVersion.Parse("1.0.0"), Digest('a'), new InstanceId("default"), [1],
        "hello-process", ModuleTarget.Runtime, "pid:42", Digest('b'), "module-nonce");

    private static RuntimeChallenge Challenge(ModuleHello hello) =>
        new(1, "runtime-nonce", hello.Nonce, "hmac-sha256", Now.AddSeconds(-1), Now.AddMinutes(1));

    private static ControlMessage Control(ControlMessageKind kind) =>
        new("operation-1", kind, Now.AddMinutes(1), JsonSerializer.SerializeToElement(new { }));

    private static string Digest(char value) => $"sha256:{new string(value, 64)}";
}
