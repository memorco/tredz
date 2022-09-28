namespace Tredz.DataAccess.Blob.Services;

public class DeleteBlobService : IDeleteBlobService
{
    private readonly ICreateBlobServiceClientService _createServiceClientService;

    public DeleteBlobService(ICreateBlobServiceClientService createServiceClientService)
    {
        _createServiceClientService = createServiceClientService;
    }


    public async Task<bool> DeleteBlobAsync(DeleteFileRequest deleteRequest)
    {
        var containerClient = _createServiceClientService.CreateBlobContainerClient(deleteRequest.Container);
        var blobClient = containerClient.GetBlobClient(deleteRequest.Filename);

        var response = await blobClient.DeleteAsync();

        return response.Status is 200 or 202;
    }
}