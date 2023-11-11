using System.ComponentModel.DataAnnotations;

namespace Contracts.Dto.Geolocation;

public class GeolocationRequestDto
{
    [Required]
    [Display(Name = "userId")]
    public string UserId { get; set; } = default!;
}