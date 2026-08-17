namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Classifies a contract change for versioning purposes.</summary>
public enum ContractChangeKind
{
    /// <summary>A backwards-compatible correction.</summary>
    Patch,
    /// <summary>A backwards-compatible addition.</summary>
    Additive,
    /// <summary>A breaking change.</summary>
    Breaking
}
