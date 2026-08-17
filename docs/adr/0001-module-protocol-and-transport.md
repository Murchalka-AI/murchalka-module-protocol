# ADR-0001: Module Protocol and transport

Status: Accepted

## Decision

Murchalka uses one transport-neutral semantic protocol with explicit version negotiation. Local process, container, remote, and WASM adapters may encode it differently, but cannot alter capability semantics. gRPC is the canonical streaming RPC representation; JSON is the diagnostics, fixtures, and local framing representation.

Startup is a fail-closed state machine: `ModuleHello → RuntimeChallenge → ModuleProof → snapshots/endpoints → ModuleReady → Activate`. Capability traffic is illegal before `Activate`; failed proof or an invalid transition faults the session permanently.

The proof covers both nonces and verified module identity material. A production Runtime supplies an authenticated proof verifier bound to the bundle identity and transport peer. Phase 0 defines that boundary and a deterministic HMAC verifier for conformance tests; key issuance belongs to Root Trust in Phase 1.

## Consequences

Wire contracts can evolve separately from transports. Every implementation must enforce frame limits, deadlines, cancellation, identifiers, declared capability digests, and state transitions. Transport liveness never implies module readiness or authorization.
