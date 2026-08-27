namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Reports a module rollout state for one Node.</summary>
/// <param name="DeploymentId">The deployment identifier.</param>
/// <param name="NodeId">The target Node.</param>
/// <param name="Artifact">The selected artifact.</param>
/// <param name="State">The deployment state.</param>
/// <param name="UpdatedAt">The last state transition time.</param>
/// <param name="ReasonCode">The normalized reason code.</param>
public sealed record NodeDeploymentStatus(Guid DeploymentId, NodeId NodeId, NodeArtifactDescriptor Artifact,
    NodeDeploymentState State, DateTimeOffset UpdatedAt, string? ReasonCode);
