using Contracts.Dto.MediatR;
using MediatR;

namespace Services.Abstraction.Cqrs.Commands;

public interface ICommand: IRequest<Result>
{
}

public interface ICommand<TResponse>: IRequest<Result<TResponse>>
{
}