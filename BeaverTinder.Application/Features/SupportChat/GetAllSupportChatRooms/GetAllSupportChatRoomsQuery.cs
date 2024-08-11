using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.SupportChat.GetAllSupportChatRooms;

public class GetAllSupportChatRoomsQuery : IQuery<IEnumerable<SupportRoom>>
{
    
}