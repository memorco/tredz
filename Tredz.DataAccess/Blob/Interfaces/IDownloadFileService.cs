namespace Tredz.DataAccess.Blob.Interfaces;

public interface IDownloadFileService
{
    Task<DownloadFileResponse> DownloadFileAsync(DownloadFileRequest request);
}