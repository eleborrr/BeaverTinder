using BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Application.Features.Geolocation.GetGeolocationById;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Application.Dto.Account;
using BeaverTinder.Mobile.Errors;
using HotChocolate.Authorization;
using System.Security.Claims;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{

    [Authorize]
    public async Task<EditUserRequestDto> GetAccountInformation(HttpContext context, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var serviceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
        var id = context.User.FindFirstValue("id")!;
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
            Password = "",
            ConfirmPassword = "",
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