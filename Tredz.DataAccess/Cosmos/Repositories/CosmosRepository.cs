namespace Tredz.DataAccess.Cosmos.Repositories;

public class CosmosRepository<T> : ICosmosRepository<T> where T : CosmosEntityBase
{
    private readonly Container _container;

    public CosmosRepository(Container container)
    {
        _container = container;
    }

    public async Task<T> GetItemAsync(string id)
    {
        try
        {
            return await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id));
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return default;
        }
    }

    public async Task InsertItemAsync(T item)
    {
        await _container.CreateItemAsync(item, ResolvePartitionKey(item.id));
    }

    public async Task UpdateItemAsync(string id, T item)
    {
        await _container.UpsertItemAsync(item, ResolvePartitionKey(id));
    }

    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<T>(id, ResolvePartitionKey(id));
    }

    public PartitionKey ResolvePartitionKey(string itemId)
    {
        return new PartitionKey(itemId);
    }
}