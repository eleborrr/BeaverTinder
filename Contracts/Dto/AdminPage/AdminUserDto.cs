using System.ComponentModel.DataAnnotations;

namespace Contracts.Dto.AdminPage;

public class AdminUserDto
{
    [Required]
    [Display(Name = "Id")]
    public string Id { get; set; } = default!;
    
    [Required]
    [Display(Name = "Is Blocked")]
    public bool IsBlocked { get; set; }
    
    [Required]
    [Display(Name = "Is Searching")]
    public bool IsSearching { get; set; }
    
    [Required]
    [Display(Name = "Nickname")]
    public string UserName { get; set; } = default!;

    [Required]
    [Display(Name = "Subscription Name")]
    public string SubName { get; set; } = default!;
    [Required]
    [Display(Name = "Subscription Expires Date Time")]
    public DateTime SubExpiresDateTime { get; set; }
}