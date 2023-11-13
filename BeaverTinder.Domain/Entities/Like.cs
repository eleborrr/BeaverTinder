namespace BeaverTinder.Domain.Entities;

public class Like
{
    public int Id { get; set; }
    public string UserId { get; set; } = default!;
    public string LikedUserId {get; set; } = default!;
    public DateTime LikeDate { get; set; }
    
    public bool Sympathy { get; set; }
}