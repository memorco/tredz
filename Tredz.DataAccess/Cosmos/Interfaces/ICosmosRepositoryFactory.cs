namespace Tredz.DataAccess.Cosmos.Interfaces;

public interface ICosmosRepositoryFactory : IDisposable
{
    Container GetContainer(string databaseName, string containerName);
}