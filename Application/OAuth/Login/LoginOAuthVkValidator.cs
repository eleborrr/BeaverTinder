using FluentValidation;

namespace Application.OAth.Login;

public class LoginOAuthVkValidator : AbstractValidator<LoginOAuthVkCommand>
{
    public LoginOAuthVkValidator()
    {
        RuleFor(c => c.SignedUser).NotNull();
    }
}