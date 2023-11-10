using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class LoginDto
{
    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; } = default!;
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")] 
    public string Password { get; set; } = default!;
    
    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}