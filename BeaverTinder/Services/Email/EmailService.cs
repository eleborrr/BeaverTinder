using System.Net;
using System.Net.Mail;
using BeaverTinder.Models;
using DogApi.Models;
using DogApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;


namespace BeaverTinder.Services;

public class EmailService: IEmailServiceInterface
{
    private readonly EmailConfig ec;

    public EmailService(IOptions<EmailConfig> emailConfig)
    {
        this.ec = emailConfig.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            var from = new MailAddress(ec.FromAddress);
            var to =  new MailAddress(email);
            var host = ec.Server;
            var port = ec.Port;
            
            var emailMessage = new MailMessage(from, to);

            emailMessage.To.Add(to);
            emailMessage.Subject = subject;
            emailMessage.Body = message;
            var emailMessage2 = new MailMessage(from, from);
            emailMessage2.To.Add(from);
            emailMessage2.Subject = subject;
            emailMessage2.Body = message;
            
            using (var client = new SmtpClient(host, port))
            {
                client.Credentials = new NetworkCredential(ec.FromAddress, ec.UserPassword);
                client.EnableSsl = true;
                await client.SendMailAsync(emailMessage);
                await client.SendMailAsync(emailMessage2);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    
}