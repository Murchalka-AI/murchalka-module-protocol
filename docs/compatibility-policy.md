# Compatibility and version policy

## Protocol and schema APIs

`*/v1` API identifiers and protocol version `1` are closed sets. A peer must reject an unknown major. Additive optional fields and new compatible error codes may ship in a minor release. Removing or renaming a field, making an optional field required, changing meaning, security semantics, interaction kind, or side-effect behavior requires a new major.

Unknown fields are rejected for a known schema major unless they are inside a namespaced `extensions` object. Receivers must preserve extension values they relay but must not interpret an unknown extension as authority.

## Semantic versions and ranges

Module and capability versions use SemVer 2.0.0. Supported ranges are intersections of whitespace-separated comparators (`>=`, `>`, `<=`, `<`, `=`), an exact SemVer, `*`, or a partial stable version (`1`, `1.2`). Partial versions expand to the corresponding major or minor interval. A prerelease does not satisfy a stable range unless the range explicitly contains a prerelease comparator.

Install order and file order are never compatibility or provider-selection inputs.

## Capability evolution

- additive optional input/output fields: minor;
- compatible new normalized error: minor;
- implementation-only latency improvement: patch;
- required fields, changed meaning, changed side effects, or weaker security: major.

The schema digest is immutable for a published capability version and is recorded in `module.lock.json`.
