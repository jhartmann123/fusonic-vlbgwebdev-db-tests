using Microsoft.Extensions.DependencyInjection;

namespace FancyLibTests;

public abstract class TestBase : IDisposable, IClassFixture<TestFixture>
{
    private readonly AsyncServiceScope scope;

    protected TestBase(TestFixture fixture)
    {
        scope = fixture.BeginScope();
        GetInstance<ITestStore>().OnTestStart();
    }

    public T GetInstance<T>()
        where T : class
        => scope.ServiceProvider.GetRequiredService<T>();

    public void Dispose()
    {
        GetInstance<ITestStore>().OnTestEnd();

        scope.Dispose();
        GC.SuppressFinalize(this);
    }
}