namespace Tredz.DataAccess.Blob.Services;

public class CreateBlobServiceClientService : ICreateBlobServiceClientService
{
    private readonly string _connectionString;

    public CreateBlobServiceClientService(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException(nameof(connectionString) + 
                                        "CANNOT be NULL or WHITESPACE.  " +
                                        "Please check where you are configuring this in your application.  " +
                                        "Usually it resides in the Azure Key Vault");
        }

        _connectionString = connectionString;
    }

    public BlobServiceClient CreateBlobServiceClient()
    {
        return new(_connectionString);
    }

    public BlobContainerClient CreateBlobContainerClient(string containerName) => new(_connectionString, containerName);

    public BlobClient ContainerClientByPathParts(IEnumerable<string> pathParts, BlobContainerClient container)
    {
        var validPathParts = pathParts.Skip(1);
        var fileFolderPath = string.Join("/", validPathParts); // Ignore the container from the list which is should always be the first item

        return container.GetBlobClient(fileFolderPath);
    }
}