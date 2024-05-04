using BeaverTinder.Application.Dto.Geolocation;
using BeaverTinder.Application.Features.Geolocation.GetGeolocationById;
using BeaverTinder.Mobile.Helpers.PolicyStrings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BeaverTinder.Mobile.Graphql.Profile.Queries;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class GeolocationByIdQuery
{
    private readonly IMediator _mediator;

    public GeolocationByIdQuery(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Policy = PolicyStaticStrings.MapSubs)]
    public async Task<GeolocationResponseDto?> GetUserGeolocation(GetGeolocationRequestDto model)
    {
        return (await _mediator.Send(new GetGeolocationByIdQuery(model.UserId))).Value;
    }
}