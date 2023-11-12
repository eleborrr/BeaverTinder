using MediatR;
using Services.Abstraction.Cqrs.Commands;

namespace Application.Email.SendEmail;

public record SendEmailCommand(
    string Email,
    string Subject,
    string Message) : ICommand<Unit>;