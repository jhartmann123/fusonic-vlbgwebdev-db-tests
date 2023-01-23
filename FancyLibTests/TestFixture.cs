using FancyLib;
using FancyLib.Services;
using FancyLibTests.TestStores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Needed for TestStore1/2 (they do not support parallelism)
//[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace FancyLibTests;

public class TestFixture
{
    private readonly IServiceProvider serviceProvider;

    public TestFixture()
    {
        var services = new ServiceCollection();

        //var testStore = new TestStore1SingleDbCreatePerTest(TestSettings.ConnectionString);
        //var testStore = new TestStore2SingleDbCreatePerTestOnConnect(TestSettings.ConnectionString);
        //var testStore = new TestStore3SingleDbWithTransaction();
        var testStore = new TestStore4CopyPerTest(TestSettings.FastConnectionString);

        services.AddSingleton<ITestStore>(testStore);

        services.AddDbContext<AppDbContext>(o => o
                                                 // .UseNpgsql(TestSettings.ConnectionString)
                                                 .UseNpgsql(testStore.ConnectionString) // Needed for TestStore4 
                                                 .AddInterceptors(new ConnectionOpeningInterceptor(testStore.OnConnect)) // Needed for TestStore2-4
                                                 );

        services.AddScoped<NumberService>();
        services.AddScoped<TextService>();

        serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
    }

    public AsyncServiceScope BeginScope() => serviceProvider.CreateAsyncScope();
}