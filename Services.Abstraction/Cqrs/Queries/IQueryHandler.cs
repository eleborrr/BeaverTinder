using Contracts.Dto.MediatR;
using MediatR;

namespace Services.Abstraction.Cqrs.Queries;

public interface IQueryHandler<in TQuery, TResponse>: IRequestHandler<TQuery, Result<TResponse>> where TQuery: IQuery<TResponse> 
{
}
