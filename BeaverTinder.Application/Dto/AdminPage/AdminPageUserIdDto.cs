using System.ComponentModel.DataAnnotations;

namespace BeaverTinder.Application.Dto.AdminPage;

public class AdminPageUserIdDto
{
    [Required]
    [Display(Name = "UserId")]
    public string UserId { get; set; } = default!;
}