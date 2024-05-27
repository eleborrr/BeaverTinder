namespace BeaverTinder.Clickhouse.Helpers;

public class ClickhouseData
{
    public string ConnectionString { get; set; } = default!;
    public string DbName { get; set; } = default!;
    public string TableName { get; set; } = default!;
}