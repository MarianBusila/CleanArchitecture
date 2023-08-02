using System;
using Catalog.Application.Playlists.Queries.GetPlaylist.Filters;
using Catalog.Application.Repositories;
using Catalog.Application.Tracks.Queries.GetTrack.Filters;
using Catalog.Infrastructure.Sql.Filters;
using Catalog.Infrastructure.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.DependencyInjection
{
    public static class InfrastructureSetup
    {
        public static IServiceCollection ConfigureSql(this IServiceCollection services, string connectionString, bool isDevelopment)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string may not be null, empty, or whitespace", nameof(connectionString));

            services.AddHealthChecks()
                .AddDbContextCheck<CatalogDbContext>("Sql Database");

            return services.AddDbContext<CatalogDbContext>(options => 
            { 
                options.UseNpgsql(connectionString);
                options.EnableDetailedErrors(isDevelopment);
                options.EnableSensitiveDataLogging(isDevelopment);
            })
                .AddScoped<IPlaylistFilter, LinqPlaylistFilter>()
                .AddScoped<ITrackFilter, LinqTrackFilter>()
                .AddScoped<ICatalogRepository, CatalogRepository>();
        }
    }
}
