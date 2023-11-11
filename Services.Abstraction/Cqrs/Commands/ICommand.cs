using Contracts.Dto.MediatR;
using MassTransit;
using MediatR;

namespace Services.Abstraction.Cqrs.Commands;

public interface ICommand: IRequest<ValidationResultExtensions.Result>
{
}

public interface ICommand<TResponse>: IRequest<Result<TResponse>>
{
}