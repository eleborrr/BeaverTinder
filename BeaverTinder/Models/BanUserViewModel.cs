using System.ComponentModel.DataAnnotations;

namespace BeaverTinder.Models;

public class BanUserViewModel
{
    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }
}