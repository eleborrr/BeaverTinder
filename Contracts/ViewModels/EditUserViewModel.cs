using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels;

public class EditUserViewModel
{
    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Nickname")]
    public string UserName { get; set; }

    [Required]
    public double Latitude { get; set; }
    
    [Required]
    public double Longitude { get; set; }
    
    [Required]
    [Display(Name = "Image")]
    public string Image { get; set; }

    [Required] [Display(Name = "Gender")]
    public string Gender { get; set; }

    [Display(Name = "Tell about yourself")]
    public string About { get; set; }
    
    [Required]
    [Display(Name = "Subscription name")]
    public string SubName { get; set; }
    
    [Required]
    [Display(Name = "Subscription Date")]
    public DateTime SubExpiresDateTime { get; set; }
}