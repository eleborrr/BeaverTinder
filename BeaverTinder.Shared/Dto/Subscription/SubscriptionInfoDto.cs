namespace BeaverTinder.Shared.Dto.Subscription;

public class SubscriptionInfoDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public double PricePerMonth { get; set; }
}