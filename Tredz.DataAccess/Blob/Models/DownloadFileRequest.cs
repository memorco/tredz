namespace Tredz.DataAccess.Blob.Models;

public class DownloadFileRequest
{
    [Required]
    public string FilePath { get; set; }
}