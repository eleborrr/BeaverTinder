using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User: IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? About { get; set; }
    public string Gender { get; set; }  = default!;
    public DateTime DateOfBirth { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsSearching { get; set; }
    public string? Image { get; set; } // ? kak xranit kartinku
}