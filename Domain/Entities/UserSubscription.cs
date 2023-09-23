namespace Domain.Entities;

public class UserSubscription
{
    public string? UserId { get; set; }
    public int SubsId { get; set; }
    public DateTime Expires { get; set; }
    public bool Active { get; set; }
}