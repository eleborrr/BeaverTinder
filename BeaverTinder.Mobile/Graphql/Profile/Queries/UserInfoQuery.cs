using BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Application.Features.Geolocation.GetGeolocationById;
using BeaverTinder.Application.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BeaverTinder.Application.Dto.Account;
using Microsoft.AspNetCore.Authorization;
using BeaverTinder.Mobile.Errors;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<EditUserRequestDto> GetAccountInformation(string id, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var serviceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
        var user = await serviceManager.UserManager.FindByIdAsync(id);
        
        if (user is null)
            throw BeaverSearchError.WithMessage("User not found");

        var geolocation = (await mediator.Send(new GetGeolocationByIdQuery(id), cancellationToken)).Value;
        if (geolocation is null)
            throw BeaverSearchError
                .WithMessage("Oops! Seems like a problem... We are working on it! \n Can't find geolocation");
            
        var subInfo = (await mediator.Send(
            new GetUsersActiveSubscriptionQuery(id),
            cancellationToken)).Value;
        
        var model = new EditUserRequestDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName!,
            Image = user.Image!,
            About = user.About ?? "",
            Gender = user.Gender,
            Latitude = geolocation.Latitude,
            Longitude = geolocation.Longitude,
            SubName = subInfo!.Name,
            SubExpiresDateTime = subInfo.Expires
        };
        return model;
    }
}