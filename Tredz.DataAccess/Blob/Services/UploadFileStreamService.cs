namespace Tredz.DataAccess.Blob.Services;

public class UploadFileStreamService : IUploadFileService
{
    private readonly ICreateBlobServiceClientService _createServiceClientService;

    public UploadFileStreamService(ICreateBlobServiceClientService createServiceClientService)
    {
        _createServiceClientService = createServiceClientService;
    }

    public async Task<bool> UploadFileAsync(UploadFileRequest request)
    {
        var pathParts = request.FilePath.Split('/').Where(w => !string.IsNullOrWhiteSpace(w));
        var container = _createServiceClientService.CreateBlobContainerClient(pathParts.FirstOrDefault());
        var client = _createServiceClientService.ContainerClientByPathParts(pathParts, container);

        request.FileStream.Position = 0;

        var options = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = request.ContentType,
                CacheControl = request.CacheControl
            }
        };

        await client.UploadAsync(request.FileStream, options).ConfigureAwait(false);
        return true;
    }
}

