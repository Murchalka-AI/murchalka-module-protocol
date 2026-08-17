namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Classifies the sensitivity of event data.</summary>
public enum DataClassification
{
    /// <summary>Public information.</summary>
    Public,
    /// <summary>Internal information.</summary>
    Internal,
    /// <summary>Personal information.</summary>
    Personal,
    /// <summary>Sensitive information.</summary>
    Sensitive,
    /// <summary>Restricted information.</summary>
    Restricted
}
