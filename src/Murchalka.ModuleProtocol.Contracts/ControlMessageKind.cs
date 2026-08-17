namespace Murchalka.ModuleProtocol.Contracts;

/// <summary>Identifies a runtime control operation.</summary>
public enum ControlMessageKind
{
    /// <summary>Starts the module.</summary>
    Start,
    /// <summary>Begins graceful draining.</summary>
    Drain,
    /// <summary>Stops the module.</summary>
    Stop,
    /// <summary>Reloads configuration.</summary>
    ReloadConfiguration,
    /// <summary>Updates dependency bindings.</summary>
    UpdateBindings,
    /// <summary>Updates the permission grant.</summary>
    UpdateGrant,
    /// <summary>Requests a health report.</summary>
    HealthProbe,
    /// <summary>Exports module state.</summary>
    ExportState,
    /// <summary>Prepares a state migration.</summary>
    PrepareMigration,
    /// <summary>Commits a state migration.</summary>
    CommitMigration,
    /// <summary>Rolls back a state migration.</summary>
    RollbackMigration,
    /// <summary>Activates the module.</summary>
    Activate
}
