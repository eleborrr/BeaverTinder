using FluentValidation;

namespace Application.OAuth.Register;

public class RegisterOAuthVkValidator : AbstractValidator<RegisterOAuthVkCommand>
{
    public RegisterOAuthVkValidator()
    {
        RuleFor(c => c.UserDto).NotNull();
    }
}