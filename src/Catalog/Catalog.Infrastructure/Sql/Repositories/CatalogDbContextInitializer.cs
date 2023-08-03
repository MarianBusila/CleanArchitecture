using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Sql.Repositories;

public static class InitializerExtensions
{
    public static void InitilizeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<CatalogDbContextInitializer>();
        initializer.Initialize();
    }
}

public class CatalogDbContextInitializer
{
    private readonly ILogger<CatalogDbContextInitializer> _logger;
    private readonly CatalogDbContext _catalogDbContext;
    
    public CatalogDbContextInitializer(ILogger<CatalogDbContextInitializer> logger, CatalogDbContext catalogDbContext)
    {
        _logger = logger;
        _catalogDbContext = catalogDbContext;
    }
    
    public void Initialize()
    {
        try
        {
            _catalogDbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}