using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class SearchDto
{
    [Required]
    [Display(Name = "UserId")]
    public string UserId { get; set; }
}