namespace Tredz.DataAccess.Blob.Services;

public class StorageDirectoryMappingService : IStorageDirectoryMappingService
{
    private const string InvalidBlobName = "$$$.$$$";

    public IEnumerable<StorageDirectoryStructureResponse> MapFolderStructure(Page<BlobHierarchyItem> hierarchyItems, string containerName)
    {
        var folderStructure = new List<StorageDirectoryStructureResponse>();

        foreach (var prefix in hierarchyItems.Values.Where(w => w.IsPrefix).Select(s => s.Prefix))
        {
            // Purpose  :   Ensuring we don't include any invalid directories or the root folder.
            if (prefix.Equals("/", StringComparison.InvariantCultureIgnoreCase))
            {
                continue;
            }

            var prefixes = prefix.Split('/').Where(w => !string.IsNullOrWhiteSpace(w));

            folderStructure.Add(new StorageDirectoryStructureResponse
            {
                Path = prefix,
                Name = prefixes.LastOrDefault(),
                StorageType = CloudStorageTypes.Folder,
            });
        }

        return folderStructure;
    }

    public IEnumerable<StorageDirectoryStructureResponse> MapFileStructure(Page<BlobHierarchyItem> hierarchyItems, IEnumerable<string> allowedMimeTypes, string containerName)
    {
        var folderStructure = new List<StorageDirectoryStructureResponse>();

        foreach (var blob in hierarchyItems.Values.Where(w => w.IsBlob).Select(s => s.Blob))
        {
            // Purpose  :   Cloud Storage fills the name with the entire path, for the purpose of this we only
            //              want to return the file name and not the whole path.
            var blobNames = blob.Name.Split('/').Where(w => !string.IsNullOrWhiteSpace(w));
            var fileName = blobNames.LastOrDefault();

            if (!allowedMimeTypes.Any(a => a == blob.Properties.ContentType) || fileName.Contains(InvalidBlobName))
            {
                continue;
            }

            folderStructure.Add(new StorageDirectoryStructureResponse
            {
                Name = fileName,
                StorageType = CloudStorageTypes.File,
                Path = $"{containerName}/{blob.Name}"
            });
        }

        return folderStructure;
    }

    public IEnumerable<DirectoryStructureBreadcrumbsResponse> MapDirectoryBreadcrumbs(string containerName, string folderPath)
    {
        var folderHasNoValue = string.IsNullOrWhiteSpace(folderPath); // Scenario :   Will happen when viewing the root container

        var folderStructure = new List<DirectoryStructureBreadcrumbsResponse>
        {
            new()
            {
                FolderPath = null,
                FolderName = containerName,
                IsActive = folderHasNoValue
            }
        };
            
        if (folderHasNoValue)
        {
            return folderStructure;
        }

        var folders = folderPath.Split('/').Where(w => !string.IsNullOrWhiteSpace(w));

        foreach (var folder in folders)
        {
            var currentFolderIndex = folderPath.IndexOf(folder);
            var substringedFolderPath = folderPath.Substring(0, currentFolderIndex); // Substring the path as the current iteration could have folders after it which we do not want.

            folderStructure.Add(new DirectoryStructureBreadcrumbsResponse
            {
                FolderName = folder,
                IsActive = folder == folders.LastOrDefault(), // The last item in the list is always the active folder
                FolderPath = string.IsNullOrEmpty(substringedFolderPath) ? $"{folder}/" : $"{substringedFolderPath}{folder}/",
            });
        }

        return folderStructure;
    }
}