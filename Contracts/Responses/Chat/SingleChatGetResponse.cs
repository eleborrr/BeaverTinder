namespace Contracts.Responses.Chat;

public class SingleChatGetResponse
{
    public string SenderId { get; set; }
    public string RecieverId { get; set; }
    public string RoomName { get; set; }
}