namespace Tredz.DataAccess.Blob.Interfaces;

public interface IStorageDirectoryStructureService
{
    Task<StorageDirectoryResponse> RetrieveDirectoryStructureAsync(StorageDirectoryStructureRequest request);
}