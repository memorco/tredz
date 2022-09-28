namespace Tredz.DataAccess.Blob.Services;

public class DownloadFileService : IDownloadFileService
{
    private readonly ICreateBlobServiceClientService _createServiceClientService;

    public DownloadFileService(ICreateBlobServiceClientService createServiceClientService)
    {
        _createServiceClientService = createServiceClientService;
    }

    public async Task<DownloadFileResponse> DownloadFileAsync(DownloadFileRequest request)
    {
        var pathParts = request.FilePath.Split('/')
            .Where(w => !string.IsNullOrWhiteSpace(w));
        var container = _createServiceClientService.CreateBlobContainerClient(pathParts.FirstOrDefault());
        var client = _createServiceClientService.ContainerClientByPathParts(pathParts, container);

        var memoryStream = new MemoryStream();

        try
        {
            // Important    :   If the container, directory or file doesn't exist the 'DownloadToAsync' throws an exception therefore we
            //                  gracefully terminate the request.
            var result = await client.DownloadToAsync(memoryStream);

            if (result.Status != 200 && result.Status != 206)
            {
                return null;
            }

            memoryStream.Position = 0; // Reset the stream position so we view it on the front end.

            var validPathParts = pathParts.Skip(1);
            var fileDetails = container.GetBlobs(prefix: string.Join("/", validPathParts)).FirstOrDefault();

            return new DownloadFileResponse
            {
                Blob = memoryStream,
                ContentType = fileDetails.Properties.ContentType
            };
        }
        catch
        {
            // TODO :   Useful to have logging here as the reasoning behind an exception is the service being down or a programatic issue
            //          with the container, directory or file name.

            await memoryStream.DisposeAsync();
        }

        return null;
    }
}