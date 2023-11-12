using Services.Abstraction.Cqrs.Commands;

namespace Application.Subscription.AddSubscription;

public class AddSubscriptionCommand: ICommand
{
    public int SubscriptionId { get; set; } = default!;
    public string UserId { get; set; } = default!;
}