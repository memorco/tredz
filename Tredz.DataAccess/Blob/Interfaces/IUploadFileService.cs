namespace Tredz.DataAccess.Blob.Interfaces;

public interface IUploadFileService
{
    Task<bool> UploadFileAsync(UploadFileRequest request);
}
