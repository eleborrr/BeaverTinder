using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class EditUserDto
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

    // [Required]
    // [Display(Name = "Email")]
    // [EmailAddress]
    // public string Email { get; set; }

    [Required]
    public double Latitude { get; set; }
    
    [Required]
    public double Longitude { get; set; }
    
    [Required]
    [Display(Name = "Image")]
    public string Image { get; set; }
    
    
    // [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "ConfirmPassword")]
    public string ConfirmPassword { get; set; }

    [Required] [Display(Name = "Gender")] public string Gender { get; set; }

    [Display(Name = "Tell about yourself")]
    public string About { get; set; }
    
    [Required]
    [Display(Name = "Subscription Name")]
    public string SubName { get; set; }
    [Required]
    [Display(Name = "Subscription Expires Date Time")]
    public DateTime SubExpiresDateTime { get; set; }
}