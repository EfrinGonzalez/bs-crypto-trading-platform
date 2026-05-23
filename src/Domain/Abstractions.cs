namespace BsCryptoTrading.Domain;

public interface IBlockchainAdapter
{
    string Chain { get; }
    Task<BroadcastResult> BroadcastAsync(SignedTransaction tx, CancellationToken ct);
    Task<TransactionStatus> GetTransactionStatusAsync(string txId, CancellationToken ct);
}

public interface IBitcoinAdapter : IBlockchainAdapter
{
    Task<FeeQuote> EstimateFeeAsync(int targetBlocks, CancellationToken ct);
    Task<IReadOnlyCollection<Utxo>> GetSpendableUtxosAsync(string address, CancellationToken ct);
}

public interface IEthereumAdapter : IBlockchainAdapter
{
    Task<GasQuote> EstimateGasAsync(string from, string to, decimal amount, string? tokenContract, CancellationToken ct);
    Task<long> GetNextNonceAsync(string address, CancellationToken ct);
}

public sealed record BroadcastResult(string TxId, bool Accepted, string? Error);
public sealed record SignedTransaction(string HexPayload, string Asset, string Network);
public sealed record TransactionStatus(string TxId, int Confirmations, string State, bool IsReorged);
public sealed record FeeQuote(long SatsPerVByte);
public sealed record GasQuote(long MaxFeePerGas, long PriorityFeePerGas, long GasLimit);
public sealed record Utxo(string TxId, int Vout, long AmountSats, string ScriptPubKey);
