using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Models;

public class User: IdentityUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool Confirmed { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string About { get; set; }
    public string Gender { get; set; }  //?
    public DateTime DateOfBirth { get; set; }
    public int ImageId { get; set; } // ? kak xranit kartinku
    public DateTime LastLoginDate { get; set; }
    public int RoleId { get; set; }
}