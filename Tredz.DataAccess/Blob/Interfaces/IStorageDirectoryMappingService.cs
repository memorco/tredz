using Azure;
using Azure.Storage.Blobs.Models;

namespace Tredz.DataAccess.Blob.Interfaces;

public interface IStorageDirectoryMappingService
{
    IEnumerable<DirectoryStructureBreadcrumbsResponse> MapDirectoryBreadcrumbs(string containerName, string folderPath);

    IEnumerable<StorageDirectoryStructureResponse> MapFolderStructure(Page<BlobHierarchyItem> blobHierarchyItems, string containerName);

    IEnumerable<StorageDirectoryStructureResponse> MapFileStructure(Page<BlobHierarchyItem> blobHierarchyItems, IEnumerable<string> allowedMimeTypes, string containerName);
}