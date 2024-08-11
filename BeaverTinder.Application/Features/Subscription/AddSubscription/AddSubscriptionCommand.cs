using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace BeaverTinder.Application.Features.Subscription.AddSubscription;

public record AddSubscriptionCommand(int SubscriptionId, string UserId): ICommand;