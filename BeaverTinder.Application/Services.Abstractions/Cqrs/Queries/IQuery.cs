using BeaverTinder.Application.Dto.MediatR;
using MediatR;

namespace BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

public interface IQuery<TResponse>: IRequest<Result<TResponse>>
{
}