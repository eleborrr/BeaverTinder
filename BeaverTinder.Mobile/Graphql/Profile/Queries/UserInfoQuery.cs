using BeaverTinder.Application.Dto.Account;
using BeaverTinder.Application.Features.Geolocation.GetGeolocationById;
using BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Mobile.Errors;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BeaverTinder.Mobile.Graphql.Profile.Queries;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserInfoQuery
{
    private readonly IMediator _mediator;
    private readonly IServiceManager _serviceManager;

    public UserInfoQuery(IMediator mediator, IServiceManager serviceManager)
    {
        _mediator = mediator;
        _serviceManager = serviceManager;
    }


    public async Task<EditUserRequestDto> GetAccountInformation(string id, CancellationToken cancellationToken)
    {
        var user = await _serviceManager.UserManager.FindByIdAsync(id);
        
        if (user is null)
            throw BeaverSearchError.WithMessage("User not found");

        var geolocation = (await _mediator.Send(new GetGeolocationByIdQuery(id), cancellationToken)).Value;
        if (geolocation is null)
            throw BeaverSearchError
                .WithMessage("Oops! Seems like a problem... We are working on it! \n Can't find geolocation");
            
        var subInfo = (await _mediator.Send(
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