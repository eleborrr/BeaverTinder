using BeaverTinder.Application.Dto.Geolocation;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Features.Geolocation.GetGeolocationById;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Repositories.Abstractions;

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