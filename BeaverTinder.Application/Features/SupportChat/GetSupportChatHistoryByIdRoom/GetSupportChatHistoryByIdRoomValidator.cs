using FluentValidation;

namespace Application.SupportChat.GetSupportChatHistory;

public class GetSupportChatHistoryByIdRoomValidator : AbstractValidator<GetSupportChatHistoryByIdRoomQuery>
{
    public GetSupportChatHistoryByIdRoomValidator()
    {
        RuleFor(c => c.SupportRoomId).NotNull();
    }
}