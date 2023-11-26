using Microsoft.AspNetCore.Http;

namespace BeaverTinder.Shared.Files;

public class FileModel
{
    public IFormFile FormFile { get; set; }
}