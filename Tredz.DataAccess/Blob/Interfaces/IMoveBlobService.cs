namespace Tredz.DataAccess.Blob.Interfaces;

public interface IMoveBlobService
{
    Task<bool> MoveBlobAsync(MoveFileRequest moveRequest);
}

