using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels;

public class LikeViewModel
{
    [Required]
    [Display(Name = "LikedUserId")]
    public string LikedUserId { get; set; } = default!;
}