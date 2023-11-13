using FluentValidation;

namespace Application.SupportChat.GetChatById;

public class GetSupportChatByIdValidator : AbstractValidator<GetSupportChatByIdQuery>
{
    public GetSupportChatByIdValidator()
    {
        RuleFor(c => c.UserId).NotNull();
        RuleFor(c => c.CurUserId).NotNull();
    }
}