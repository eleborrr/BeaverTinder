using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Image
{
    public string Id { get; set; } = default!;
    public string ImageName { get; set; } = default!;
    public string ImagePath { get; set; } = default!;
    [NotMapped]
    public IFormFile ImageFile { get; set; } = default!;
}