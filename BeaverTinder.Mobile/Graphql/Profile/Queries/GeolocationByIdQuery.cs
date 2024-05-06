using BeaverTinder.Application.Features.Geolocation.GetGeolocationById;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BeaverTinder.Mobile.Helpers.PolicyStrings;
using BeaverTinder.Application.Dto.Geolocation;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = PolicyStaticStrings.MapSubs)]
    public async Task<GeolocationResponseDto?> GetUserGeolocation(GetGeolocationRequestDto model)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return (await mediator.Send(new GetGeolocationByIdQuery(model.UserId))).Value;
    }
}