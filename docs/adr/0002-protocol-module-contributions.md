# ADR-0002: Protocol module contributions

Status: Accepted

## Decision

External protocols are installed as modules. A module declares a bounded route namespace, handler capability, discovery descriptor, transport and authentication support, streaming shape, and hard limits. Runtime validates and catalogs these declarations but does not interpret protocol-specific messages.

The generic Protocol Gateway owns external listeners and dispatches only to Runtime-granted handler capabilities. MCP, A2A, and future protocols remain independently removable bundles. External content is always untrusted, private-network access is denied unless an explicit allowlist is granted, and an external peer never receives the internal capability registry.

## Consequences

Protocol routes can appear and disappear during dependency reconciliation without rebuilding Runtime. Authentication, payload validation, authorization, cancellation, observability, and audit are enforced at both the gateway boundary and protocol adapter boundary. Unknown contribution versions, route collisions, undeclared handlers, and unsupported transports fail closed.
