using Dapper;
using Npgsql;
using Polly;

namespace FancyLibTests.TestStores;

public class TestStore4CopyPerTest : ITestStore
{
    private readonly string templateDatabaseName;
    private readonly string postgresConnectionString;
    private readonly NpgsqlConnectionStringBuilder connectionStringBuilder;

    public string ConnectionString => connectionStringBuilder.ConnectionString;

    private bool dbCreated;

    public TestStore4CopyPerTest(string templateConnectionString, string maintenanceDbName = "postgres")
    {
        connectionStringBuilder = new NpgsqlConnectionStringBuilder(templateConnectionString);

        templateDatabaseName = connectionStringBuilder.Database
                            ?? throw new ArgumentException("Missing template database in connection string.");

        connectionStringBuilder.Database = maintenanceDbName;
        postgresConnectionString = connectionStringBuilder.ConnectionString;

        OnTestStart();
    }

    public void OnTestStart()
    {
        connectionStringBuilder.Database = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).TrimEnd('=');
        dbCreated = false;
    }

    public void OnConnect()
    {
        if (dbCreated)
            return;

        // Creating a DB from a template can cause an exception when done in parallel.
        // The lock usually prevents this, however, we still encounter race conditions
        // where we just have to retry.
        // 55006: source database "test_template" is being accessed by other users
        Policy.Handle<NpgsqlException>(e => e.SqlState == "55006")
              .WaitAndRetry(30, _ => TimeSpan.FromMilliseconds(500))
              .Execute(CreateDb);

        void CreateDb()
        {
            if (dbCreated)
                return;

            using var connection = new NpgsqlConnection(postgresConnectionString);
            connection.Open();
            connection.Execute($@"CREATE DATABASE ""{connectionStringBuilder.Database}"" TEMPLATE ""{templateDatabaseName}""");

            dbCreated = true;
        }
    }

    public void OnTestEnd()
    {
        if (!dbCreated)
            return;

        using var connection = new NpgsqlConnection(postgresConnectionString);
        connection.Open();
        connection.Execute($@"DROP DATABASE IF EXISTS ""{connectionStringBuilder.Database}"" WITH (FORCE)");
    }
}