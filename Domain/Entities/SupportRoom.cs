namespace Domain.Entities;

public class SupportRoom
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FirstUserId { get; set; }
    public string SecondUserId { get; set; }
    public ICollection<SupportChatMessage> Messages { get; set; }
}