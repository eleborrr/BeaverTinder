using BeaverTinder.Application.Features.SupportChat.CreateSupportChatById;
using FluentValidation;

namespace Application.SupportChat.CreateSupportChatById;

public class CreateSupportChatByIdValidator : AbstractValidator<CreateSupportChatByIdCommand>
{
    public CreateSupportChatByIdValidator()
    {
        RuleFor(c => c.CurUserId).NotNull();
        RuleFor(c => c.UserId).NotNull();
    }
}