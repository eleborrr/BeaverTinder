using System.Net;
using System.Net.Mail;
using Contracts.Configs;
using Contracts.Dto.MediatR;
using MediatR;
using Microsoft.Extensions.Options;
using Services.Abstraction.Cqrs.Commands;

namespace Application.Email.SendEmail;

public class SendEmailHandler : ICommandHandler<SendEmailCommand, Unit>
{
    private readonly EmailConfig _ec;

    public SendEmailHandler(IOptions<EmailConfig> emailConfig)
    {
        _ec = emailConfig.Value;
    }

    public async Task<Result<Unit>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var from = new MailAddress(_ec.FromAddress);
            var to =  new MailAddress(request.Email);
            var host = _ec.Server;
            var port = _ec.Port;
            
            var emailMessage = new MailMessage(from, to);

            emailMessage.To.Add(to);
            emailMessage.Subject = request.Subject;
            emailMessage.Body = request.Message;
            var emailMessage2 = new MailMessage(from, from);
            emailMessage2.To.Add(from);
            emailMessage2.Subject = request.Subject;
            emailMessage2.Body = request.Message;
            
            using (var client = new SmtpClient(host, port))
            {
                client.Credentials = new NetworkCredential(_ec.FromAddress, _ec.UserPassword);
                client.EnableSsl = true;
                await client.SendMailAsync(emailMessage, cancellationToken);
                await client.SendMailAsync(emailMessage2, cancellationToken);
            }

            return new Result<Unit>(new Unit(), true);
        }
        catch (Exception ex)
        {
            return new Result<Unit>(new Unit(), false, ex.Message);
        }
    }
}