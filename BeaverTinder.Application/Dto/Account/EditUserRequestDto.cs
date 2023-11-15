using System.ComponentModel.DataAnnotations;

namespace BeaverTinder.Application.Dto.Account;

public class EditUserRequestDto
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

    // -> [Required]
    // -> [Display(Name = "Email")]
    // -> [EmailAddress]
    // -> public string Email { get; set; }

    [Required]
    public double Latitude { get; set; }
    
    [Required]
    public double Longitude { get; set; }
    
    [Required]
    [Display(Name = "Image")]
    public string Image { get; set; } = default!;

    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "ConfirmPassword")]
    public string ConfirmPassword { get; set; } = default!;

    [Required] [Display(Name = "Gender")] 
    public string Gender { get; set; } = default!;

    [Display(Name = "Tell about yourself")]
    public string About { get; set; } = default!;
    
    [Required]
    [Display(Name = "Subscription Name")]
    public string SubName { get; set; } = default!;
    [Required]
    [Display(Name = "Subscription Expires Date Time")]
    public DateTime SubExpiresDateTime { get; set; }
}