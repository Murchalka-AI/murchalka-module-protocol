namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Defines the state of a module deployment on one Node.</summary>
public enum NodeDeploymentState
{
    /// <summary>The state is not recognized by this protocol version.</summary>
    Unknown,
    /// <summary>The deployment is queued.</summary>
    Pending,
    /// <summary>The artifact is being transferred.</summary>
    Distributing,
    /// <summary>The artifact is installed but not active.</summary>
    Installed,
    /// <summary>The Node module is active and healthy.</summary>
    Active,
    /// <summary>The deployment failed.</summary>
    Failed,
    /// <summary>The Node is offline and deployment is deferred.</summary>
    Offline,
    /// <summary>The previous artifact was restored.</summary>
    RolledBack
}
