using System.Data.Common;
using Catalog.Api;
using Catalog.Infrastructure.Sql.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Catalog.Application.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public ITestOutputHelper Output { get; set; }
    
    private readonly DbConnection _connection;

    public CustomWebApplicationFactory(DbConnection connection)
    {
        _connection = connection;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders(); // Remove other loggers
        });
        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<CatalogDbContext>>()
                .RemoveAll<CatalogDbContextInitializer>()
                .AddDbContext<CatalogDbContext>(options =>
                {
                    options.UseNpgsql(_connection);
                });

        });
    }
}