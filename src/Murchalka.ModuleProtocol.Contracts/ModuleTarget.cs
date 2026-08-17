namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies the host tier targeted by a module.</summary>
public enum ModuleTarget
{
    /// <summary>The central runtime.</summary>
    Runtime,
    /// <summary>A node runtime.</summary>
    Node,
    /// <summary>A client runtime.</summary>
    Client
}
