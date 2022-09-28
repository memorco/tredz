namespace Tredz.DataAccess.Blob.Models;

public class StorageDirectoryStructureResponse
{
    public string Name { get; set; }

    public string Path { get; set; }

    public CloudStorageTypes StorageType { get; set; }
}