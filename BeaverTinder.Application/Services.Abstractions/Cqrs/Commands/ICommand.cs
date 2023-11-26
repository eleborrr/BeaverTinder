using BeaverTinder.Application.Dto.MediatR;
using MediatR;

namespace BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

public interface ICommand: IRequest<Result>
{
}

public interface ICommand<TResponse>: IRequest<Result<TResponse>>
{
}