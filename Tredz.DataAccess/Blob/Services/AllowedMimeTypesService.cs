namespace Tredz.DataAccess.Blob.Services;

public class AllowedMimeTypesService : IAllowedMimeTypesService
{
    public IEnumerable<string> AllowedMimeTypes(AllowedFileTypes allowedFileType)
    {
        var response = new List<string>();

        switch (allowedFileType)
        {
            case AllowedFileTypes.StandardImageTypes:
                response.Add(MimeTypeConstants.BMP);
                response.Add(MimeTypeConstants.PNG);
                response.Add(MimeTypeConstants.JPEG);
                break;
            default:
                throw new ArgumentOutOfRangeException($"The filetype provided (Filetype: '{allowedFileType}') is not supported.");
        }

        return response;
    }
}