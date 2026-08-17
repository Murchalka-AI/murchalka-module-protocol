# Murchalka Module Protocol

Phase 0 implementation of the versioned Runtime-to-Module boundary. This repository contains no product behavior.

It publishes transport-neutral contracts, JSON and gRPC representations, compatibility rules, the protocol session state machine, and the canonical JSON Schemas used to validate module metadata.

## Build

```sh
dotnet restore --locked-mode
dotnet test --no-restore
```

The supported wire protocol major is `1`. Unknown protocol or schema majors fail closed. See [Compatibility policy](docs/compatibility-policy.md) and [ADR-0001](docs/adr/0001-module-protocol-and-transport.md).

## Canonical schemas

- `module-manifest.schema.json`
- `module-lock.schema.json`
- `capability.schema.json`
- `binding.schema.json`
- `profile.schema.json`
- `permission-grant.schema.json`
- `event-envelope.schema.json`

All schemas use JSON Schema Draft 2020-12, reject unknown fields, and reserve the `extensions` object for namespaced additions.
