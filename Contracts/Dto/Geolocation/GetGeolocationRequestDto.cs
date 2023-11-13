using System.ComponentModel.DataAnnotations;

namespace Contracts.Dto.Geolocation;

public class GetGeolocationRequestDto
{
    [Required]
    [Display(Name = "userId")]
    public string UserId { get; set; } = default!;
}