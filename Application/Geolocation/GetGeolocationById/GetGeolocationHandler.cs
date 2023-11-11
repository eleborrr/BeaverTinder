using Contracts.Dto.Geolocation;
using Contracts.Dto.MediatR;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Geolocation.GetGeolocationById;

public class GetGeolocationHandler: IQueryHandler<GetGeolocationByIdQuery, GeolocationResponseDto>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetGeolocationHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<GeolocationResponseDto>> Handle(GetGeolocationByIdQuery request, CancellationToken cancellationToken)
    {
        var geolocation = await _repositoryManager.GeolocationRepository.GetByUserIdAsync(request.UserId);

        if (geolocation is null)
            return new Result<GeolocationResponseDto>(
                null,
                false,
                "Geolocation not found");

        return new Result<GeolocationResponseDto>(
            new GeolocationResponseDto(geolocation.Id, geolocation.UserId, geolocation.Longitude, geolocation.Latitude), true, null);
    }
}