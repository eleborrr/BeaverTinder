namespace BeaverTinder.Application.Configs;

public class EmailConfig
{
    public string FromName { get; set; } = default!;
    public string FromAddress { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string Server { get; set; } = default!;
    public int Port { get; set; }
    public string UserPassword { get; set; } = default!;
}