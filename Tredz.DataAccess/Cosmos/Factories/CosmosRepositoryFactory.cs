namespace Tredz.DataAccess.Cosmos.Factories;

public class CosmosRepositoryFactory : ICosmosRepositoryFactory
{
    private readonly CosmosClient _cosmosClient;

    public CosmosRepositoryFactory(string connectionString)
    {
        _cosmosClient = new CosmosClient(connectionString);

    }

    public Container GetContainer(string databaseName, string containerName)
    {
        return _cosmosClient
            .GetDatabase(databaseName)
            .GetContainer(containerName);
    }

    public void Dispose() => _cosmosClient?.Dispose();
}