using BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Shared.Dto.Subscription;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    [Authorize]
    public async Task<UserSubscriptionDto> GetUserSubInformation(
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var subInfo = (await mediator.Send(
            new GetUsersActiveSubscriptionQuery(userId),
            cancellationToken)).Value;
        
        var model = new UserSubscriptionDto
        {
            Name = subInfo!.Name,
            Expires = subInfo.Expires
        };
        return model;
    }
}