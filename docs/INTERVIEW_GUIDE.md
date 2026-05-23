# Interview Guide
## 5-minute walkthrough
1. Business goals and constraints.
2. Context boundaries and custody/trading separation.
3. Event-driven order-to-settlement flow.
4. Security model with key isolation.
5. Operability: observability, reconciliation, incident readiness.

## 10-minute deep dive
- CQRS and outbox idempotency.
- Blockchain abstraction for BTC UTXO vs ETH account model.
- Withdrawal control plane, approvals, and risk checks.
- Reconciliation and reorg recovery strategy.
- Scalability plan for microservice extraction.

## Sample interview questions
- Solution architecture: Which context do you split first and why?
- .NET backend: How do you implement idempotent MediatR handlers?
- Kafka: How do you handle poison messages and replay?
- Kubernetes: How do you scale consumers safely?
- Blockchain: How do you design reorg-safe deposit accounting?
- Custody: What controls separate key custody from trading operations?
- Stakeholder communication: How do you explain eventual consistency risk to COO/CRO?
