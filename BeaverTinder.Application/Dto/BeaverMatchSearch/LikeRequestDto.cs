using System.ComponentModel.DataAnnotations;

namespace BeaverTinder.Application.Dto.BeaverMatchSearch;

public class LikeRequestDto
{
    [Required]
    [Display(Name = "LikedUserId")]
    public string LikedUserId { get; set; } = default!;
}