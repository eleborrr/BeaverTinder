using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;
using MassTransit;
using MediatR;

namespace BeaverTinder.Application.Features.SupportChat.SaveMessageByDtoBus;

public class SaveMessageByDtoBusHandler : ICommandHandler<SaveMessageByDtoBusCommand, Unit>
{
    private readonly IBus _bus;

    public SaveMessageByDtoBusHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task<Result<Unit>> Handle(
        SaveMessageByDtoBusCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = new SupportChatMessage
            {
                SenderId = request.Message.SenderId,
                ReceiverId = request.Message.ReceiverId,
                Content = request.Message.Content,
                Timestamp = request.Message.Timestamp,
                RoomId = request.Message.RoomId
            };
            await _bus.Publish(entity, cancellationToken);
            return new Result<Unit>(new Unit(), true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Result<Unit>(new Unit(), false, e.Message);
        }
        
    }
}