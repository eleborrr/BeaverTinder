using Contracts.Dto.MediatR;
using MediatR;

namespace Services.Abstraction.Cqrs.Queries;

public interface IQuery<TResponse>: IRequest<Result<TResponse>>
{
}