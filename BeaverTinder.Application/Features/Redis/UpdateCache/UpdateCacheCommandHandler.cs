// using BeaverTinder.Application.Dto.MediatR;
// using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
// using StackExchange.Redis;
//
// namespace BeaverTinder.Application.Features.Redis.UpdateCache;
//
// public class UpdateCacheCommandHandler: ICommandHandler<UpdateCacheCommand>
// {
//     private readonly IConnectionMultiplexer _connectionMultiplexer;
//
//     public UpdateCacheCommandHandler(IConnectionMultiplexer connectionMultiplexer)
//     {
//         _connectionMultiplexer = connectionMultiplexer;
//     }
//
//     public Task<Result> Handle(UpdateCacheCommand request, CancellationToken cancellationToken)
//     {
//         throw new NotImplementedException();
//         // var db = _connectionMultiplexer.GetDatabase();
//         // db.
//     }
// }