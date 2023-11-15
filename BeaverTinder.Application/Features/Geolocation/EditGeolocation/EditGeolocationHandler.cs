using Application.Geolocation.EditGeolocation;
using BeaverTinder.Application.Dto.Geolocation;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.Geolocation.EditGeolocation;

public class EditGeolocationHandler: ICommandHandler<EditGeolocationCommand, GeolocationIdDto>
{
    private readonly IRepositoryManager _repositoryManager;

    public EditGeolocationHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<GeolocationIdDto>> Handle(EditGeolocationCommand request, CancellationToken cancellationToken)
    {
        var geolocation = await _repositoryManager.GeolocationRepository.GetByUserIdAsync(request.UserId);
        if (geolocation is null)
            return new Result<GeolocationIdDto>(null, false, "Geolocation not found");

        MapGeolocation(geolocation, request);
        
        //TODO что то возвращать из менеджера?
        await _repositoryManager.GeolocationRepository.UpdateAsync(geolocation);

        return new Result<GeolocationIdDto>(
            new GeolocationIdDto(geolocation.Id), true, null);
    }

    private static void MapGeolocation(UserGeolocation entity, EditGeolocationCommand input)
    {
        if (input.Longitude is not null)
            entity.Longitude = input.Longitude.Value;
        if (input.Latitude is not null)
            entity.Latitude = input.Latitude.Value;
    }
}