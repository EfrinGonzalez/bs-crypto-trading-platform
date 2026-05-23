# Bitcoin Suisse Trading Platform - Reference Architecture

Enterprise-grade, interview-ready reference implementation of a **crypto brokerage + custody platform** using .NET 9, Kafka, PostgreSQL, Kubernetes, and Azure deployment patterns.

## Scope
- Simulated multi-asset brokerage (BTC, ETH, ERC-20, stablecoins)
- Custody ledger separated from trading state
- Blockchain abstraction (`IBlockchainAdapter`, `IBitcoinAdapter`, `IEthereumAdapter`)
- CQRS + MediatR + Outbox + event-driven integration
- Blazor operational dashboard

## Repository Structure
- `src/Api` - ASP.NET Core API (BFF + orchestration boundary)
- `src/Application` - CQRS handlers, workflows, policy orchestration
- `src/Domain` - aggregates, value objects, domain events
- `src/Infrastructure` - EF Core, Kafka, blockchain adapters, outbox worker
- `src/Dashboard` - Blazor Web App for operations and observability
- `tests/ArchitectureTests` - architecture and slice-level tests
- `docs` - architecture artifacts, ADRs, diagrams, interview kit
- `deploy` - Docker, K8s base/overlays, cloud-ready assets

## Quick Start
```bash
docker compose -f deploy/docker/docker-compose.yml up -d
# run api
cd src/Api && dotnet run
# run dashboard
cd src/Dashboard && dotnet run
```

## Documentation Index
- [Architecture](docs/ARCHITECTURE.md)
- [Security Model](docs/SECURITY_MODEL.md)
- [Deployment](docs/DEPLOYMENT.md)
- [Observability](docs/OBSERVABILITY.md)
- [Trade-offs](docs/TRADE_OFFS.md)
- [Interview Guide](docs/INTERVIEW_GUIDE.md)
- [ADRs](docs/adr)
