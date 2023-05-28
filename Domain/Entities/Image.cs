using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Image
{
    public string Id { get; set; }
    public string ImageName { get; set; }
    public string ImagePath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
}