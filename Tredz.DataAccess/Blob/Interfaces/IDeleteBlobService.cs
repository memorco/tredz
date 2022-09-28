namespace Tredz.DataAccess.Blob.Interfaces;

public interface IDeleteBlobService
{
    Task<bool> DeleteBlobAsync(DeleteFileRequest deleteRequest);
}