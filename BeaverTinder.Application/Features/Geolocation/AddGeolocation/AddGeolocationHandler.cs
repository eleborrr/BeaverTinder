using Application.Geolocation.AddGeolocation;
using BeaverTinder.Application.Dto.Geolocation;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.Geolocation.AddGeolocation;

public class AddGeolocationHandler: ICommandHandler<AddGeolocationCommand, GeolocationIdDto>
{
    private readonly IRepositoryManager _repositoryManager;

    public AddGeolocationHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<GeolocationIdDto>> Handle(AddGeolocationCommand request, CancellationToken cancellationToken)
    {
        var newGeolocation = new UserGeolocation()
        {
            UserId = request.UserId,
            Longitude = request.Longitude,
            Latitude = request.Latitude
        };
        
        // TODO нужно ли что то возвращать из репозитория, чтобы отлавливать ошибки?
        var id = await _repositoryManager.GeolocationRepository.AddAsync(newGeolocation);

        return new Result<GeolocationIdDto>(
            new GeolocationIdDto(id), true, null);
    }
}