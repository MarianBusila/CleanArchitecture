using System;
using System.Linq;
using Catalog.Infrastructure.Sql.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Catalog.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public ITestOutputHelper Output { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders(); // Remove other loggers
                })
                .ConfigureTestServices((services) =>
                {
                    // don't run IHostedServices when running as a test
                    services.RemoveAll(typeof(IHostedService));

                    // use in memory database
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CatalogDbContext>));

                    services.Remove(descriptor);
                    services.RemoveAll<CatalogDbContextInitializer>();

                    services.AddDbContext<CatalogDbContext>(options => { options.UseInMemoryDatabase("InMemoryDbForTesting"); });
                    
                    /*
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .AddEnvironmentVariables()
                        .Build();

                    services.AddDbContext<CatalogDbContext>(options =>
                    {
                        options.UseNpgsql(configuration.GetConnectionString("chinook"));
                    });
                    */

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<CatalogDbContext>();
                        var logger = scopedServices
                            .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                        db.Database.EnsureCreated();

                        try
                        {
                            logger.LogInformation("Seeding database...");
                            CatalogSeed.InitializeDbForTests(db);
                            logger.LogInformation("... Done");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the " +
                                "database with test messages. Error: {Message}", ex.Message);
                            throw;
                        }
                    }

                });
        }

    }
}
