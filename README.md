# Murchalka Module Protocol

Phase 0 implementation of the versioned Runtime-to-Module boundary. This repository contains no product behavior.

It publishes transport-neutral contracts, JSON and gRPC representations, compatibility rules, the protocol session state machine, and the canonical JSON Schemas used to validate module metadata.

## Build

```sh
dotnet restore
dotnet test --no-restore
```

## CI and releases

GitHub Actions validates the solution on Linux, Windows, and macOS. Pull requests and pushes to `main` run locked restore, Release build, tests, and a NuGet packaging check. The resulting CI packages are retained as workflow artifacts for 14 days.

Pushing a SemVer tag publishes all five protocol packages to GitHub Packages and creates or updates a GitHub Release with the `.nupkg` files attached:

```sh
git tag v0.1.12
git push origin v0.1.12
```

Tags may use `vX.Y.Z` or `vX.Y.Z-prerelease`. The tag version is applied to every package so the protocol package set is always released atomically with one version. Publishing uses the repository `GITHUB_TOKEN`; no long-lived package secret is stored in the repository.

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
