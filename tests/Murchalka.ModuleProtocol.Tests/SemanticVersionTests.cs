using Murchalka.ModuleProtocol.Contracts;

namespace Murchalka.ModuleProtocol.Tests;

public sealed class SemanticVersionTests
{
    [Theory]
    [InlineData("1.0.0", "1.0.1")]
    [InlineData("1.0.0-alpha", "1.0.0")]
    [InlineData("1.0.0-alpha.1", "1.0.0-alpha.beta")]
    [InlineData("1.9.9", "2.0.0")]
    public void Comparison_follows_semver_precedence(string lower, string higher) =>
        Assert.True(SemanticVersion.Parse(lower) < SemanticVersion.Parse(higher));

    [Theory]
    [InlineData(">=1.3.0 <2.0.0", "1.3.0", true)]
    [InlineData(">=1.3.0 <2.0.0", "2.0.0", false)]
    [InlineData("1", "1.99.0", true)]
    [InlineData("1.2", "1.3.0", false)]
    [InlineData(">=1.0.0 <2.0.0", "1.1.0-beta.1", false)]
    [InlineData(">=1.1.0-beta.1 <2.0.0", "1.1.0-beta.2", true)]
    public void Ranges_are_deterministic(string range, string candidate, bool expected) =>
        Assert.Equal(expected, VersionRangeExpression.Parse(range).Satisfies(SemanticVersion.Parse(candidate)));

    [Fact]
    public void Unknown_protocol_major_fails_closed() =>
        Assert.Throws<ProtocolNegotiationException>(() => CompatibilityPolicy.NegotiateProtocol([2, 3]));
}
