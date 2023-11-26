using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace Application.Subscription.AddSubscription;

public record AddSubscriptionCommand(int SubscriptionId, string UserId): ICommand;