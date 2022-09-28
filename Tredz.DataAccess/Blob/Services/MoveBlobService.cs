namespace Tredz.DataAccess.Blob.Services;

public class MoveBlobService : IMoveBlobService
{
    private readonly ICreateBlobServiceClientService _createServiceClientService;

    public MoveBlobService(ICreateBlobServiceClientService createServiceClientService)
    {
        _createServiceClientService = createServiceClientService;
    }

    public async Task<bool> MoveBlobAsync(MoveFileRequest moveRequest)
    {
        var sourceContainerClient = _createServiceClientService.CreateBlobContainerClient(moveRequest.SrcContainer);
        var sourceBlobClient = sourceContainerClient.GetBlobClient(moveRequest.SrcFilename);

        var targetContainerClient = _createServiceClientService.CreateBlobContainerClient(moveRequest.DstContainer);
        var targetBlobClient = targetContainerClient.GetBlobClient(moveRequest.DstFilename);

        await targetBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri);

        var operation = await targetBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri);
        await operation.WaitForCompletionAsync();

        var response = await sourceBlobClient.DeleteAsync();

        return (response.Status == 200 || response.Status == 202);
    }
}

