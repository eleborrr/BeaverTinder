using BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Shared.Dto.Subscription;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeaverTinder.Mobile.Graphql.Profile.Queries;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserSubInfoQuery
{
    private readonly IMediator _mediator;

    public UserSubInfoQuery(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<UserSubscriptionDto> GetUserSubInformation(
        [FromQuery] string userId,
        CancellationToken cancellationToken)
    {
        var subInfo = (await _mediator.Send(
            new GetUsersActiveSubscriptionQuery(userId),
            cancellationToken)).Value;
        
        var model = new UserSubscriptionDto()
        {
            Name = subInfo!.Name,
            Expires = subInfo.Expires
        };
        return model;
    }
}