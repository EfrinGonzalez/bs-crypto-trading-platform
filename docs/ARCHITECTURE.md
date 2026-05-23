# Architecture

## Executive Summary
This platform is designed as a **modular monolith with microservice-ready boundaries**. It optimizes delivery speed for an interview/reference context while preserving strong bounded-context isolation for future extraction.

## Bounded Contexts
1. Identity & Client
2. Wallet & Custody
3. Market Data
4. Order Management
5. Execution Gateway
6. Settlement
7. Blockchain Monitoring
8. Risk & Compliance
9. Reconciliation
10. Observability & Audit

## C4 - Context Diagram
```mermaid
graph TD
  Client[Institutional Client] --> API[Trading API]
  Ops[Operations Team] --> Dashboard[Blazor Ops Dashboard]
  API --> Kafka[(Kafka)]
  API --> PG[(PostgreSQL)]
  API --> BTC[Bitcoin Node Provider]
  API --> ETH[Ethereum Node Provider]
  API --> KMS[Key Vault / HSM]
  Dashboard --> API
```

## C4 - Container Diagram
```mermaid
graph LR
  subgraph Platform
    API[ASP.NET Core API]
    APP[Application Layer CQRS]
    DOM[Domain Model]
    INF[Infrastructure Layer]
    WORKERS[Background Workers]
    DASH[Blazor Dashboard]
  end
  API-->APP-->DOM
  APP-->INF
  INF-->PG[(PostgreSQL)]
  INF-->K[(Kafka)]
  WORKERS-->K
  WORKERS-->PG
  INF-->BTC
  INF-->ETH
  DASH-->API
```

## C4 - Component Diagram (Trading + Custody)
```mermaid
graph TD
  OMS[Order Management Component] --> Risk[Risk Validator]
  Risk --> Exec[Execution Simulator/Gateway]
  Exec --> Settlement[Settlement Orchestrator]
  Settlement --> Custody[Custody Ledger]
  Custody --> ChainAbs[Blockchain Adapter Facade]
  ChainAbs --> BtcAdapter[NBitcoin Adapter]
  ChainAbs --> EthAdapter[Nethereum Adapter]
```

## Sequence - Order Lifecycle
```mermaid
sequenceDiagram
  participant C as Client
  participant API as Trading API
  participant OMS as Order Service
  participant R as Risk
  participant EX as Execution Gateway
  participant S as Settlement
  C->>API: Submit Order
  API->>OMS: OrderSubmitted
  OMS->>R: Validate limits/exposure
  alt Risk failed
    R-->>OMS: RiskCheckFailed
  else Risk passed
    OMS->>EX: Route order
    EX-->>OMS: OrderExecuted
    OMS->>S: Start settlement
    S-->>OMS: TradeSettled
  end
```

## Sequence - Deposit Workflow
```mermaid
sequenceDiagram
  participant Node as Blockchain Monitor
  participant Cust as Custody Service
  participant Led as Custody Ledger
  Node->>Cust: BlockchainTransactionDetected
  Cust->>Led: Record pending deposit
  Cust-->>Node: DepositPending published
  Node->>Cust: Confirmations reached
  Cust->>Led: Move pending->available
  Cust-->>Node: DepositConfirmed published
```

## Sequence - Withdrawal Workflow
```mermaid
sequenceDiagram
  participant Cl as Client
  participant API as API
  participant Cust as Custody
  participant Sign as Signing Service
  participant Ch as Chain Adapter
  Cl->>API: Withdrawal request
  API->>Cust: WithdrawalRequested
  Cust->>Sign: Sign transaction
  Sign->>Ch: BroadcastAsync
  Ch-->>Cust: txId
  Cust-->>API: WithdrawalBroadcast
  Ch-->>Cust: confirmations
  Cust-->>API: WithdrawalConfirmed
```

## Sequence - Settlement Workflow
```mermaid
sequenceDiagram
  participant Ex as Execution
  participant Set as Settlement
  participant Led as Custody Ledger
  Ex->>Set: OrderExecuted
  Set->>Led: Reserve/Release balances
  Set->>Led: Post settlement entries
  Set-->>Ex: TradeSettled
```

## Sequence - Blockchain Monitoring
```mermaid
sequenceDiagram
  participant W as Worker
  participant B as Bitcoin Adapter
  participant E as Ethereum Adapter
  participant K as Kafka
  W->>B: poll mempool/blocks
  W->>E: poll logs/receipts
  B-->>K: BlockchainTransactionDetected
  E-->>K: BlockchainTransactionDetected
  W-->>K: BlockchainReorgDetected
```

## Sequence - Reconciliation
```mermaid
sequenceDiagram
  participant Recon as Reconciliation Engine
  participant Led as Custody Ledger
  participant Chain as Adapters
  Recon->>Led: Read internal balances
  Recon->>Chain: Read on-chain balances
  Recon->>Led: Write reconciliation entries
  Recon-->>Led: CustodyLedgerReconciled
```

## Sequence - Risk Validation
```mermaid
sequenceDiagram
  participant OMS as Order Management
  participant Risk as Risk Engine
  participant Limits as Limits Store
  OMS->>Risk: Validate(order)
  Risk->>Limits: fetch limits+exposure
  alt breach
    Risk-->>OMS: RiskCheckFailed
  else pass
    Risk-->>OMS: OrderValidated
  end
```

## Infrastructure Diagrams
### Azure Topology
```mermaid
graph TD
  AFD[Azure Front Door/WAF]-->AKS[AKS Cluster]
  AKS-->PG[Azure PostgreSQL Flexible Server]
  AKS-->AKV[Azure Key Vault]
  AKS-->EH[Azure Monitor / Log Analytics]
  AKS-->ACR[Azure Container Registry]
```

### Kubernetes Topology
```mermaid
graph LR
  Ingress-->ApiPod[api deployment]
  Ingress-->DashPod[dashboard deployment]
  ApiPod-->WorkerPod[outbox/blockchain/recon workers]
  ApiPod-->PG[(postgres stateful service)]
  ApiPod-->Kafka[(kafka cluster)]
```

### Kafka Event Topology
```mermaid
graph LR
  MarketData-->T1[market-price-updated]
  OMS-->T2[order-submitted/validated/executed]
  Settlement-->T3[trade-settled]
  Custody-->T4[deposit/withdrawal events]
  Monitor-->T5[blockchain events]
  Recon-->T6[custody-ledger-reconciled]
```

### Multi-chain Adapter
```mermaid
graph TD
  App[Application Services]-->Facade[IBlockchainAdapter]
  Facade-->BTC[IBitcoinAdapter/NBitcoin]
  Facade-->ETH[IEthereumAdapter/Nethereum]
  BTC-->BTCN[Bitcoin RPC/Indexer]
  ETH-->ETHN[Ethereum RPC]
```

### Hot/Cold Wallet Architecture
```mermaid
graph LR
  Trading-->Hot[Hot Wallet Pool]
  Hot-->Signer[Online Signer]
  Signer-->Chain[Broadcast]
  Hot-->Sweep[Sweep Jobs]
  Sweep-->Cold[Cold Wallet Vault]
  Cold-->Manual[Offline approval + HSM]
```
