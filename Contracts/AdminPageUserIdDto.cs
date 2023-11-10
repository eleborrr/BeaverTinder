using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class AdminPageUserIdDto
{
    [Required]
    [Display(Name = "UserId")]
    public string UserId { get; set; } = default!;
}