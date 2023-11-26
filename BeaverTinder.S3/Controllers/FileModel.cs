using Microsoft.AspNetCore.Http;

namespace BeaverTinder.Application.Services.S3;

public class FileModel
{
    public IFormFile FormFile { get; set; }

}