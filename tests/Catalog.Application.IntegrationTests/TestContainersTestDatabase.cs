using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Respawn;
using Ardalis.GuardClauses;
using Catalog.Infrastructure.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn.Graph;
using Testcontainers.PostgreSql;

namespace Catalog.Application.IntegrationTests;

public class TestContainersTestDatabase : ITestDatabase
{
    private readonly PostgreSqlContainer _container;
    private string _connectionString = null!;
    private DbConnection _connection = null!;
    private Respawner _respawner = null!;

    public TestContainersTestDatabase()
    {
        _container = new PostgreSqlBuilder()
            .WithAutoRemove(true)
            .Build();
    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _container.GetConnectionString();
        
        _connection = new NpgsqlConnection(_connectionString);
        await _connection.OpenAsync();
        
        // migrate database
        var options = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseNpgsql(_connectionString)
            .Options;
        var context = new CatalogDbContext(options);
        await context.Database.MigrateAsync();

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "music_catalog"},
            TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
        });
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connection);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }    
}