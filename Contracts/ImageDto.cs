using Microsoft.AspNetCore.Http;

namespace Contracts;

public class ImageDto
{
    public Guid Id { get; set; }
    public string ImageName { get; set; }
    public IFormFile ImageFile { get; set; }
}