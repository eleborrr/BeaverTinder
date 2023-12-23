using Application.SupportChat.GetSupportChatHistory;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Dto.SupportChat;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Features.SupportChat.GetSupportChatHistoryByIdRoom;

public class GetSupportChatHistoryByIdRoomHandler : IQueryHandler<GetSupportChatHistoryByIdRoomQuery, IEnumerable<ChatMessageDto>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<User> _userManager;

    public GetSupportChatHistoryByIdRoomHandler(IRepositoryManager repositoryManager, UserManager<User> userManager)
    {
        _repositoryManager = repositoryManager;
        _userManager = userManager;
    }

    public async Task<Result<IEnumerable<ChatMessageDto>>> Handle(GetSupportChatHistoryByIdRoomQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var messages = _repositoryManager.SupportChatMessageRepository.GetAll().Where(msg => msg.RoomId == request.SupportRoomId)
                .ToList();
            if (messages.Count == 0)
                return new Result<IEnumerable<ChatMessageDto>>(Array.Empty<ChatMessageDto>(), true);
            var result = await Task.WhenAll(messages.Select(async m => new ChatMessageDto()
            {
                Timestamp = m.Timestamp,
                Content = m.Content,
                RoomId = m.RoomId,
                ReceiverId = m.ReceiverId,
                SenderId = m.SenderId,
                SenderName = (await _userManager.FindByIdAsync(m.SenderId))!.UserName!,
                ReceiverName = (await _userManager.FindByIdAsync(m.ReceiverId))!.UserName!
            }));
            return new Result<IEnumerable<ChatMessageDto>>(result, true);
        }
        catch (Exception e)
        {
            return new Result<IEnumerable<ChatMessageDto>>(null, false, e.Message);
        }
        
    }
}