using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Shared.Dto.Subscription;

namespace BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;

public record GetUsersActiveSubscriptionQuery(string UserId): IQuery<UserSubscriptionDto>;