using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? About { get; set; }
    public string Gender { get; set; }  //?
    public DateTime DateOfBirth { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsSearching { get; set; }
    public string? Image { get; set; } // ? kak xranit kartinku
}