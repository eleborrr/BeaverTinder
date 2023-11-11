using Contracts.Dto.Geolocation;
using Contracts.Dto.MediatR;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Commands;

namespace Features.Geolocation.AddGeolocation;

public class AddGeolocationHandler: ICommandHandler<AddGeolocationCommand, GeolocationRequestDto>
{
    private readonly IRepositoryManager _repositoryManager;

    public AddGeolocationHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public Task<Result<GeolocationRequestDto>> Handle(AddGeolocationCommand request, CancellationToken cancellationToken)
    {
        _repositoryManager
    }
}