namespace Tredz.DataAccess.Blob.Interfaces;

public interface ICreateBlobServiceClientService
{
    BlobServiceClient CreateBlobServiceClient();

    BlobContainerClient CreateBlobContainerClient(string containerName);

    BlobClient ContainerClientByPathParts(IEnumerable<string> pathParts, BlobContainerClient container);
}