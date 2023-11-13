using FluentValidation;

namespace Application.OAuth.GetUserFromToVkById;

public class GetUserFromVkByIdValidator : AbstractValidator<GetUserFromVkByIdQuery>
{
    public GetUserFromVkByIdValidator()
    {
        RuleFor(q => q.VkId).NotNull();

        RuleFor(q => q.VkId).NotEmpty();
    }
}