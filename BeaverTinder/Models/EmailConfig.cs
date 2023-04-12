namespace DogApi.Models;

public class EmailConfig
{
    public string FromName { get; set; }
    public string FromAddress { get; set; }
    public string UserId { get; set; }
    public string Server { get; set; }
    public int Port { get; set; }
    public string UserPassword { get; set; }
}