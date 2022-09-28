namespace Tredz.DataAccess.Blob.Interfaces;

public interface IAllowedMimeTypesService
{
    IEnumerable<string> AllowedMimeTypes(AllowedFileTypes allowedFileType);
}