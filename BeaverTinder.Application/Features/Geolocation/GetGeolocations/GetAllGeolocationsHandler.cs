using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.Geolocation.GetGeolocations;

public class GetAllGeolocationsHandler: IQueryHandler<GetAllGeolocationsQuery, IEnumerable<UserGeolocation>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetAllGeolocationsHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<IEnumerable<UserGeolocation>>> Handle(GetAllGeolocationsQuery request, CancellationToken cancellationToken)
    {
        var geolocations = await _repositoryManager.GeolocationRepository.GetAllAsync(cancellationToken);

        return new Result<IEnumerable<UserGeolocation>>(
            geolocations, true, null);
    }
}