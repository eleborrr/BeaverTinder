using FluentValidation;

namespace Application.OAth.AddUserToVk;

public class AddUserToVkValidator : AbstractValidator<AddUserToVkCommand>
{
    public AddUserToVkValidator()
    {
        RuleFor(c => c.PlatformUserId).NotEmpty();
        RuleFor(c => c.PlatformUserId).NotNull();
        
        RuleFor(c => c.VkUserId).NotEmpty();
        RuleFor(c => c.VkUserId).NotNull();
    }
}