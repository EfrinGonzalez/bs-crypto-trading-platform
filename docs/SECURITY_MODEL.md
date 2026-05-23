# Security Model
- Private keys isolated in signing boundary; API never handles raw keys.
- HSM-ready via Key Vault + Managed HSM abstraction.
- RBAC with least privilege per context.
- Withdrawal approval policy: 4-eyes for high value + velocity/risk checks.
- Separation of duties: trading state services cannot sign chain transactions.
- End-to-end audit trail: immutable audit events with correlation IDs.
- Secret management via Kubernetes CSI + Key Vault references.
