using FluentValidation;

namespace Application.OAth.Register;

public class RegisterOAuthVkValidator : AbstractValidator<RegisterOAuthVkCommand>
{
    public RegisterOAuthVkValidator()
    {
        RuleFor(c => c.UserDto).NotNull();
    }
}