namespace Services.Abstraction.Cqrs.Commands;

public interface ICommandHandler<in TCommand>: IRequestHandler<TCommand, Result> where TCommand: ICommand //TODO: Inherit certain interface 
{
}

public interface ICommandHandler<in TCommand, TResponse>: IRequestHandler<TCommand, Result<TResponse>> where TCommand: ICommand<TResponse> //TODO: Inherit certain interface 
{
}