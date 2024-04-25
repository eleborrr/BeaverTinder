namespace BeaverTinder.Subscription.Core.Entities;

public class UserSubscription
{
    public string UserId { get; set; } = null!;
    public int SubId { get; set; }
    public int RoleId { get; set; }
    public string Description { get; set; }
    public double PricePerMonth { get; set; }
    public string RoleName { get; set; }
    public DateTime Expires { get; set; }
    public bool Active { get; set; }
}