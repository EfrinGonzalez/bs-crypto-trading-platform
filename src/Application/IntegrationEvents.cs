namespace BsCryptoTrading.Application.Events;

public interface IIntegrationEvent
{
    Guid EventId { get; }
    DateTimeOffset OccurredAtUtc { get; }
    string CorrelationId { get; }
}

public sealed record MarketPriceUpdated(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, string Symbol, decimal Bid, decimal Ask) : IIntegrationEvent;
public sealed record OrderSubmitted(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, Guid OrderId) : IIntegrationEvent;
public sealed record OrderValidated(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, Guid OrderId) : IIntegrationEvent;
public sealed record OrderExecuted(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, Guid OrderId, decimal FillPrice, decimal FilledQuantity) : IIntegrationEvent;
public sealed record TradeSettled(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, Guid TradeId) : IIntegrationEvent;
public sealed record DepositPending(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, string TxId, string Asset) : IIntegrationEvent;
public sealed record DepositConfirmed(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, string TxId, int Confirmations) : IIntegrationEvent;
public sealed record WithdrawalRequested(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, Guid WithdrawalId) : IIntegrationEvent;
public sealed record WithdrawalBroadcast(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, Guid WithdrawalId, string TxId) : IIntegrationEvent;
public sealed record WithdrawalConfirmed(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, Guid WithdrawalId) : IIntegrationEvent;
public sealed record BlockchainTransactionDetected(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, string Chain, string TxId) : IIntegrationEvent;
public sealed record BlockchainReorgDetected(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, string Chain, long ReorgFromHeight) : IIntegrationEvent;
public sealed record CustodyLedgerReconciled(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, DateOnly BusinessDate) : IIntegrationEvent;
public sealed record RiskCheckFailed(Guid EventId, DateTimeOffset OccurredAtUtc, string CorrelationId, Guid OrderId, string Reason) : IIntegrationEvent;
