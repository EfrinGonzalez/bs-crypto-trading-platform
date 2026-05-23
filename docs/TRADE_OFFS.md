# Trade-offs
- Modular monolith chosen for delivery speed; boundaries are extraction-ready.
- Kafka adds ops overhead but gives replay, decoupling, and backpressure control.
- Eventual consistency accepted between trading/custody for resilience/scalability.
- PostgreSQL chosen for transactional integrity and operational familiarity.
- Simulated execution gateway retained as seam for venue/FIX integration.
