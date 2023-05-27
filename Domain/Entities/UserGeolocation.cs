namespace Domain.Entities;

public class UserGeolocation
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public double Latutide { get; set; }
    public double Longtitude { get; set; }
}