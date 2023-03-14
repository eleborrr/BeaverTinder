using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Models;

public class User: IdentityUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Sex Sex { get; set; }  //?
    public int ImageId { get; set; } // ? kak xranit kartinku
    public string About { get; set; }
}


public class Sex
{
    public bool IsMan { get; set; } //default = true
}