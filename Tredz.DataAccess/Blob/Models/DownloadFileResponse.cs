namespace Tredz.DataAccess.Blob.Models;

public class DownloadFileResponse
{
    public string ContentType { get; set; }

    public MemoryStream Blob { get; set; }
}