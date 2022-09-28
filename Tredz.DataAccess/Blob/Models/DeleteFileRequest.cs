namespace Tredz.DataAccess.Blob.Models;

public class DeleteFileRequest
{
    public string Container { get; set; }

    public string Filename { get; set; }
}