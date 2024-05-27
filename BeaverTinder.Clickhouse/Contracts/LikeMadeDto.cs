namespace BeaverTinder.Clickhouse.Contracts;

public class LikeMadeDto
{
    public uint Id { get; set; }
    public DateTime Date { get; set; }
    public long CountLikes { get; set; }
}