namespace Services.Abstraction.Email;

public interface IEmailService
{
    public Task SendEmailAsync(string email, string subject, string message);
}