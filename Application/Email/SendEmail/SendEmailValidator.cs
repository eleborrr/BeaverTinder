using FluentValidation;

namespace Application.Email.SendEmail;

public class SendEmailValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailValidator()
    {
        RuleFor(c => c.Email).NotNull();

        RuleFor(c => c.Message).NotNull();

        RuleFor(c => c.Subject).NotNull();
    }
}