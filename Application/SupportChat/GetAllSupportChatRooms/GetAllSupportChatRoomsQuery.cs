using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.SupportChat.GetAllSupportChatRooms;

public class GetAllSupportChatRoomsQuery : IQuery<IEnumerable<SupportRoom>>
{
    
}