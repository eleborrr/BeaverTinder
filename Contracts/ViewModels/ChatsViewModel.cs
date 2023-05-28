using Domain.Entities;

namespace Contracts.ViewModels;

public class ChatsViewModel
{
    public IEnumerable<User> Chats { get; set; }
}