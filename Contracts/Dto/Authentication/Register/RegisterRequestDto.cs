using System.ComponentModel.DataAnnotations;

namespace Contracts.Dto.Authentication.Register;

public class RegisterRequestDto
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
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "ConfirmPassword")]
    public string ConfirmPassword { get; set; } = default!;
    
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    public DateTime DateOfBirth { get; set; } = default!;

    [Required]
    public double Latitude { get; set; } = default!;
    
    [Required]
    public double Longitude { get; set; } = default!;
    
    [Required] [Display(Name = "Gender")] 
    public string Gender { get; set; } = default!;

    [Display(Name = "Tell about yourself")]
    public string About { get; set; } = default!;
}