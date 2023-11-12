using FluentValidation;
namespace Application.TwoFa.ConfirmEmail;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailValidator()
    {
        RuleFor(c => c.UserEmail).NotNull();

        RuleFor(c => c.Token).NotNull();
    }
}