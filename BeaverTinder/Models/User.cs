namespace BeaverTinder.Models;

public class User
{
    public Guid Id { get; set; }
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