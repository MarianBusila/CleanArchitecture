using Catalog.Infrastructure.Sql.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests;

public class Testing
{
    private readonly ITestDatabase _database;
    private readonly CustomWebApplicationFactory _factory;
    private readonly IServiceScopeFactory _scopeFactory;

    public Testing()
    {
        _database = TestDatabaseFactory.CreateAsync().Result;

        _factory = new CustomWebApplicationFactory(_database.GetConnection());

        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }
    
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }
    
    public async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public async Task ResetState()
    {
        await _database.ResetAsync();
    }

    public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        return await context.FindAsync<TEntity>(keyValues);
    }

    public void SetOutput(ITestOutputHelper output)
    {
        _factory.Output = output;
    }
}