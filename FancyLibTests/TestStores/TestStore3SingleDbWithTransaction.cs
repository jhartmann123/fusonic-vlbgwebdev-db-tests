using System.Transactions;
using FancyLib;

namespace FancyLibTests.TestStores;

public class TestStore3SingleDbWithTransaction : ITestStore, IDisposable
{
    private TransactionScope? transactionScope;

    public void OnTestStart() => transactionScope = TransactionUtil.CreateTransactionScope();

    public void OnConnect() { }

    public void OnTestEnd() => transactionScope?.Dispose();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        transactionScope?.Dispose();
    }
}