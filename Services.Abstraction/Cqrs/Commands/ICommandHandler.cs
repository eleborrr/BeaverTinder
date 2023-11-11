using Contracts.Dto.MediatR;
using MassTransit;
using MediatR;

namespace Services.Abstraction.Cqrs.Commands;

public interface ICommandHandler<in TCommand>: IRequestHandler<TCommand, ValidationResultExtensions.Result> where TCommand: ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse>: IRequestHandler<TCommand, Result<TResponse>> where TCommand: ICommand<TResponse>
{
}