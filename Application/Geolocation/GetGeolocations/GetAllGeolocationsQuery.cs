using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Geolocation.GetGeolocations;

public class GetAllGeolocationsQuery: IQuery<IEnumerable<UserGeolocation>>
{
    
}