using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels;

public class GeolocationRequestViewModel
{
    [Required]
    [Display(Name = "userId")]
    public string UserId { get; set; }
}