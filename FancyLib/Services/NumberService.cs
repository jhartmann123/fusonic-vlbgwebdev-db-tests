using System.Transactions;
using FancyLib.Domain;

namespace FancyLib.Services;

public class NumberService
{
    private readonly AppDbContext dbContext;

    public NumberService(AppDbContext dbContext) => this.dbContext = dbContext;

    public int CreateNumber(int number)
    {
        if (number < 0)
            throw new ArgumentOutOfRangeException(nameof(number), "Because of reasons");

        var entity = new Number { Value = number };
        dbContext.Add(entity);
        dbContext.SaveChanges();

        return entity.Id;
    }



























































    public int CreateNumberWithoutTransaction(int number)
    {
        using var scope = TransactionUtil.CreateTransactionScope(TransactionScopeOption.Suppress);
        return CreateNumber(number);
    }
}