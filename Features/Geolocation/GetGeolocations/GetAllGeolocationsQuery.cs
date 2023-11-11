using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Features.Geolocation.GetGeolocations;

public class GetAllGeolocationsQuery: IQuery<IEnumerable<UserGeolocation>>
{
    
}