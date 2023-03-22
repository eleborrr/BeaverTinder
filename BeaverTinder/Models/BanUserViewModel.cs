using System.ComponentModel.DataAnnotations;

namespace BeaverTinder.Models;

public class BanUserViewModel
{
    [Required]
    [Display(Name = "UserId")]
    public string UserId { get; set; }
}