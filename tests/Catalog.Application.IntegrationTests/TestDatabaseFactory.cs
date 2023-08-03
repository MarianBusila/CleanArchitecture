namespace Catalog.Application.IntegrationTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        //var database = new PostgresTestDatabase();
        var database = new TestContainersTestDatabase();

        await database.InitialiseAsync();

        return database;
    }
}