namespace Tredz.DataAccess.Blob.Services;

public class StorageDirectoryStructureService : IStorageDirectoryStructureService
{
    private readonly ICreateBlobServiceClientService _createServiceClientService;
    private readonly IStorageDirectoryMappingService _storageDirectoryMappingService;

    public StorageDirectoryStructureService(ICreateBlobServiceClientService createServiceClientService, IStorageDirectoryMappingService storageDirectoryMappingService)
    {
        _createServiceClientService = createServiceClientService;
        _storageDirectoryMappingService = storageDirectoryMappingService;
    }

    public async Task<StorageDirectoryResponse> RetrieveDirectoryStructureAsync(StorageDirectoryStructureRequest request)
    {
        var container = _createServiceClientService.CreateBlobContainerClient(request.ContainerName);

        var directoryStructure = new List<StorageDirectoryStructureResponse>();

        var prefix = !string.IsNullOrEmpty(request.FolderPath) ? request.FolderPath : "";

        try
        {
            // Important    :   Only return the current directory level for performance reasons. Addtionally, if the container doesn't
            //                  exist the method throws an exception therefore we gracefully terminate the request.
            await foreach (var page in container.GetBlobsByHierarchyAsync(prefix: prefix, delimiter: "/").AsPages())
            {
                directoryStructure.AddRange(_storageDirectoryMappingService.MapFolderStructure(page, request.ContainerName));
                directoryStructure.AddRange(_storageDirectoryMappingService.MapFileStructure(page, request.AllowedMimeTypes, request.ContainerName));
            }
        }
        catch
        {
            // TODO :   Useful to have logging here as the reasoning behind an exception is the service being down or a programatic issue
            //          with the container name.
        }

        return new StorageDirectoryResponse
        {
            DirectoryStructure = directoryStructure,
            Breadcrumbs = _storageDirectoryMappingService.MapDirectoryBreadcrumbs(request.ContainerName, request.FolderPath)
        };
    }
}