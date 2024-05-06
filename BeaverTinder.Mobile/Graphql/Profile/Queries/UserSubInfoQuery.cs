﻿using BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BeaverTinder.Shared.Dto.Subscription;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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