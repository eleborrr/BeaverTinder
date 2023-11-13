using Services.Abstraction.Cqrs.Commands;

namespace Application.Subscription.AddSubscription;

public record AddSubscriptionCommand(int SubscriptionId, string userId): ICommand;