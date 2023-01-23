using FancyLib;
using Microsoft.EntityFrameworkCore;

namespace FancyLibTests.TestStores;

public class TestStore1SingleDbCreatePerTest : ITestStore
{
    private readonly string connectionString;
    public TestStore1SingleDbCreatePerTest(string connectionString) => this.connectionString = connectionString;

    public void OnTestStart()
    {
        using var dbContext = CreateAppDbContext();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    public void OnTestEnd()
    {
        using var dbContext = CreateAppDbContext();
        dbContext.Database.EnsureDeleted();
    }

    public void OnConnect() { }

    private AppDbContext CreateAppDbContext() => new(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(connectionString).Options);
}