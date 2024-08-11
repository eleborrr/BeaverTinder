namespace BeaverTinder.Shared.Dto.Subscription;

public class UserSubscriptionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Expires { get; set; }
}