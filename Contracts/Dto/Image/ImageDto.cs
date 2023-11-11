using Microsoft.AspNetCore.Http;

namespace Contracts.Dto.Image;

public class ImageDto
{
    public Guid Id { get; set; }
    public string ImageName { get; set; } = default!;
    public IFormFile ImageFile { get; set; } = default!;
}