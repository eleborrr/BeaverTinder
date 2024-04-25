using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace BeaverTinder.Application.Features.Subscription.RemoveUserSubscription;

public record RemoveUserSubscriptionCommand(string userId, int subId) : ICommand;