using FluentValidation;

namespace Application.OAuth.GetVkUserInfo;

public class GetVkUserInfoValidator : AbstractValidator<GetVkUserInfoQuery>
{
    public GetVkUserInfoValidator()
    {
        RuleFor(q => q.AccessToken).NotNull();
    }
}