using BeaverTinder.Application.Dto.MediatR;
using MediatR;

namespace BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

public interface IQueryHandler<in TQuery, TResponse>: IRequestHandler<TQuery, Result<TResponse>> where TQuery: IQuery<TResponse> 
{
}
