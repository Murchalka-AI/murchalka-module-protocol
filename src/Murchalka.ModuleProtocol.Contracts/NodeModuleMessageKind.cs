namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies a Node module process protocol message.</summary>
public enum NodeModuleMessageKind
{
    /// <summary>The kind is not recognized by this protocol version.</summary>
    Unknown,
    /// <summary>The process declares its authenticated identity and capabilities.</summary>
    Hello,
    /// <summary>The Node Runtime authorizes the process to accept work.</summary>
    Activate,
    /// <summary>The Node Runtime requests graceful draining.</summary>
    Drain,
    /// <summary>The Node Runtime requests process shutdown.</summary>
    Stop,
    /// <summary>The Node Runtime invokes a capability.</summary>
    Invoke,
    /// <summary>The process reports invocation progress or completion.</summary>
    Result,
    /// <summary>The Node Runtime cancels an invocation.</summary>
    Cancel,
    /// <summary>The process reports health.</summary>
    Health
}
