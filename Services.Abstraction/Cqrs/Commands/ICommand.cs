namespace Services.Abstraction.Cqrs.Commands;

public interface ICommand: IRequest<Result>  //TODO: Inherit certain interface 
{
}

public interface ICommand<TResponse>: IRequest<Result<TResponse>> //TODO: Inherit certain interface 
{
}