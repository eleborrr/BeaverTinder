using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.Geolocation.GetGeolocations;

public class GetAllGeolocationsQuery: IQuery<IEnumerable<UserGeolocation>>
{
    
}