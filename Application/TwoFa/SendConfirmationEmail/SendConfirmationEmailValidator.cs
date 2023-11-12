using FluentValidation;

namespace Application.TwoFa.SendConfirmationEmail;

public class SendConfirmationEmailValidator : AbstractValidator<SendConfirmationEmailQuery>
{
    public SendConfirmationEmailValidator()
    {
        RuleFor(c => c.UserId).NotNull();
    }
    
}