using FancyLib;
using FancyLib.Services;
using FluentAssertions;

namespace FancyLibTests.Services;

public class NoTransactionTests : TestBase
{
    private readonly NumberService numberService;
    private readonly AppDbContext dbContext;

    public static IEnumerable<object[]> PositiveNumbers => Enumerable.Range(1, 100).Select(n => new object[] { n });

    public NoTransactionTests(TestFixture fixture) : base(fixture)
    {
        numberService = GetInstance<NumberService>();
        dbContext = GetInstance<AppDbContext>();
    }

    [Theory]
    [MemberData(nameof(PositiveNumbers))]
    public void CreateNumber_AddsNumberToDatabase(int number)
    {
        var id = numberService.CreateNumberWithoutTransaction(number);

        dbContext.ChangeTracker.Clear();

        var count = dbContext.Numbers.Count();
        count.Should().Be(2);

        var entity = dbContext.Numbers.Find(id);
        entity.Should().NotBeNull();
        entity!.Value.Should().Be(number);
    }
}

public class Deadlock1 : TestBase
{
    private readonly AppDbContext dbContext;

    public static IEnumerable<object[]> PositiveNumbers => Enumerable.Range(1, 100).Select(n => new object[] { n });

    public Deadlock1(TestFixture fixture) : base(fixture) => dbContext = GetInstance<AppDbContext>();

    [Theory]
    [MemberData(nameof(PositiveNumbers))]
    public void ForgedDeadlock(int number)
    {
        dbContext.Texts.Find(9001)!.Value = number.ToString();
        dbContext.SaveChanges();
        Task.Delay(20).Wait();

        dbContext.Numbers.Find(9001)!.Value = number;
        dbContext.SaveChanges();
    }
}

public class Deadlock2 : TestBase
{
    private readonly AppDbContext dbContext;

    public static IEnumerable<object[]> PositiveNumbers => Enumerable.Range(1, 100).Select(n => new object[] { n });

    public Deadlock2(TestFixture fixture) : base(fixture) => dbContext = GetInstance<AppDbContext>();

    [Theory]
    [MemberData(nameof(PositiveNumbers))]
    public void ForgedDeadlock(int number)
    {
        dbContext.Numbers.Find(9001)!.Value = number;
        dbContext.SaveChanges();

        Task.Delay(20).Wait();

        dbContext.Texts.Find(9001)!.Value = number.ToString();
        dbContext.SaveChanges();
    }
}
