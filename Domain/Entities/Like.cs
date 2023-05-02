namespace Domain.Entities;

public class Like
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string LikedUserId {get; set; }
    public DateTime LikeDate { get; set; }
    
    public bool Sympathy { get; set; }
}