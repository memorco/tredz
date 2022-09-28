namespace Tredz.DataAccess.Blob.Models;

public class StorageDirectoryResponse
{
    public IEnumerable<DirectoryStructureBreadcrumbsResponse> Breadcrumbs { get; set; }
    public IEnumerable<StorageDirectoryStructureResponse> DirectoryStructure { get; set; }
}