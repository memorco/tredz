namespace Tredz.DataAccess.Blob.Models;

public class StorageDirectoryStructureRequest
{
    /// <summary>
    /// Folder path with / as the delimiter for each folder name.
    /// </summary>
    public string FolderPath { get; set; }

    public string ContainerName { get; set; }

    public IEnumerable<string> AllowedMimeTypes { get; set; }
}