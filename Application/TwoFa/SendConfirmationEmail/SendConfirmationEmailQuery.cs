using MediatR;
using Services.Abstraction.Cqrs.Queries;

namespace Application.TwoFa.SendConfirmationEmail;

public class SendConfirmationEmailQuery : IQuery<Unit>
{
    public string UserId { get; set; }
}