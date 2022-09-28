namespace Tredz.DataAccess.Cosmos.Interfaces;

public interface ICosmosRepository<T>
{
    Task<T> GetItemAsync(string id);
    Task InsertItemAsync(T item);
    Task UpdateItemAsync(string id, T item);
    Task DeleteItemAsync(string id);

    PartitionKey ResolvePartitionKey(string itemId);
}