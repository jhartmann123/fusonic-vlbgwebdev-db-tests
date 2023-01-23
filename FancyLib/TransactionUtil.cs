using System.Transactions;

namespace FancyLib;

public static class TransactionUtil
{
    private static readonly TransactionOptions DefaultTransactionOptions = new()
    {
        IsolationLevel = IsolationLevel.ReadCommitted,
        Timeout = TransactionManager.MaximumTimeout
    };

    public static TransactionScope CreateTransactionScope(TransactionScopeOption option = TransactionScopeOption.Required)
        => new(option, DefaultTransactionOptions, TransactionScopeAsyncFlowOption.Enabled);
}