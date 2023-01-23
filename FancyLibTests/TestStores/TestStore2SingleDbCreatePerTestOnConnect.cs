using FancyLib;
using Microsoft.EntityFrameworkCore;

namespace FancyLibTests.TestStores;

public class TestStore2SingleDbCreatePerTestOnConnect : ITestStore
{
    private bool dbCreated;

    private readonly string connectionString;
    public TestStore2SingleDbCreatePerTestOnConnect(string connectionString) => this.connectionString = connectionString;

    public void OnTestStart() => dbCreated = false;

    public void OnConnect()
    {
        if (dbCreated)
            return;

        dbCreated = true;
        using var dbContext = CreateAppDbContext();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    public void OnTestEnd()
    {
        using var dbContext = CreateAppDbContext();
        dbContext.Database.EnsureDeleted();
    }

    private AppDbContext CreateAppDbContext() => new(new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(connectionString).Options);
}