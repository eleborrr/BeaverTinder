namespace Contracts.Dto.Subscription;

public class SubscriptionInfoDto
{
    public string Name { get; set; } = default!;
    public DateTime Expires { get; set; }
}