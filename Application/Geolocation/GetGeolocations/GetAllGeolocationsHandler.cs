using Contracts.Dto.Geolocation;
using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Application.Geolocation.GetGeolocationById;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Geolocation.GetGeolocations;

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