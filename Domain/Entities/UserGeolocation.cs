namespace Domain.Entities;

public class UserGeolocation
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public double Latitude { get; set; }
    public double Longtitude { get; set; }
}