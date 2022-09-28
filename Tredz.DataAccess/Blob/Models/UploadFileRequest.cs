namespace Tredz.DataAccess.Blob.Models;

public class UploadFileRequest
{
    public string FilePath { get; set; }

    public Stream FileStream { get; set; }

    public string ContentType { get; set; }

    public string CacheControl { get; set; }
}

