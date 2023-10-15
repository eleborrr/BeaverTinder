using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels;

public class EditUserViewModel
{
    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = default!;

    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = default!;

    [Required]
    [Display(Name = "Nickname")]
    public string UserName { get; set; } = default!;

    [Required]
    public double Latitude { get; set; } = default!;
    
    [Required]
    public double Longitude { get; set; } = default!;
    
    [Required]
    [Display(Name = "Image")]
    public string Image { get; set; } = default!;

    [Required] [Display(Name = "Gender")]
    public string Gender { get; set; } = default!;

    [Display(Name = "Tell about yourself")]
    public string About { get; set; } = default!;
    
    [Required]
    [Display(Name = "Subscription name")]
    public string SubName { get; set; } = default!;
    
    [Required]
    [Display(Name = "Subscription Date")]
    public DateTime SubExpiresDateTime { get; set; }
}