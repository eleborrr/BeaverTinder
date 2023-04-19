namespace Domain.Entities;

public class Like
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LikedUserId {get; set; }
    public DateTime LikeDate { get; set; }
}