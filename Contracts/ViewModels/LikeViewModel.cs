using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels;

public class LikeViewModel
{
    [Required]
    [Display(Name = "UserId")]
    public string UserId { get; set; }
    
    [Required]
    [Display(Name = "LikedUserId")]
    public string LikedUserId { get; set; }
    
    [Required]
    [Display(Name = "Sympathy")]
    public bool Sympathy { get; set; }
}