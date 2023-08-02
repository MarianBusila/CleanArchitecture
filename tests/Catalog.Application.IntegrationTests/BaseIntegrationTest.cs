using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests;

public abstract class BaseIntegrationTest : IAsyncLifetime
{
    protected readonly Testing _testing;
    
    public BaseIntegrationTest(Testing testing, ITestOutputHelper output)
    {
        _testing = testing;
        _testing.SetOutput(output);
    }
    
    public async Task InitializeAsync()
    {
        await _testing.ResetState();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}