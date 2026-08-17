# Security policy

Protocol parsers must treat manifests, envelopes, schemas, and peer messages as untrusted input. Unknown major versions, undeclared capabilities, invalid state transitions, unbounded frames, path traversal, and malformed identifiers are rejected before module execution.

Please report vulnerabilities privately to the repository maintainers. Do not include secrets or personal data in reports.
