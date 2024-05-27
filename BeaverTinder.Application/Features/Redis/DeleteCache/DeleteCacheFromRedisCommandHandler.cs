// using BeaverTinder.Application.Dto.MediatR;
// using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
// using StackExchange.Redis;
//
// namespace BeaverTinder.Application.Features.Redis.DeleteCache;
//
// public class DeleteCacheFromRedisCommandHandler: ICommandHandler<DeleteCacheFromRedisCommand>
// {
//     private readonly IConnectionMultiplexer _connectionMultiplexer;
//
//     public DeleteCacheFromRedisCommandHandler(IConnectionMultiplexer connectionMultiplexer)
//     {
//         _connectionMultiplexer = connectionMultiplexer;
//     }
//
//     public Task<Result> Handle(DeleteCacheFromRedisCommand request, CancellationToken cancellationToken)
//     {
//         try
//         {
//             var db = _connectionMultiplexer.GetDatabase();
//
//             var result = db.KeyDelete(request.DataKey);
//             
//             return Task.FromResult(new Result(result));
//         }
//         catch (Exception e)
//         {
//             return Task.FromResult(new Result(false, e.Message));
//         }
//     }
// }