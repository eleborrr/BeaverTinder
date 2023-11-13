namespace BeaverTinder.Domain.Entities;

public class UserGeolocation
{
    public string Id { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}