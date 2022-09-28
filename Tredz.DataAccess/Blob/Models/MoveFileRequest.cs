namespace Tredz.DataAccess.Blob.Models;

public class MoveFileRequest
{
    public string SrcContainer { get; set; }
    public string SrcFilename { get; set; }
    public string DstContainer { get; set; }
    public string DstFilename { get; set; }
}

