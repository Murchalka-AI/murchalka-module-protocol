namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Describes module health.</summary>
public enum ModuleHealthStatus
{
    /// <summary>The module is ready.</summary>
    Ready,
    /// <summary>The module is operational with reduced functionality.</summary>
    Degraded,
    /// <summary>The module is not ready.</summary>
    NotReady,
    /// <summary>The module is unhealthy.</summary>
    Unhealthy
}
