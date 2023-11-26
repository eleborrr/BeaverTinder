using BeaverTinder.Application.Dto.Subscription;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace Application.Subscription.GetUsersActiveSubscription;

public record GetUsersActiveSubscriptionQuery(string UserId): IQuery<SubscriptionInfoDto>;