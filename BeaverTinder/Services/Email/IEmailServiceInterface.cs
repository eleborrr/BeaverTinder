using BeaverTinder.Models;

namespace DogApi.Services;

public interface IEmailServiceInterface
{
    public Task SendEmailAsync(string email, string subject, string message);
}