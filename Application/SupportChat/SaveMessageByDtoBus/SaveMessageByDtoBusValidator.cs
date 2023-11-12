using FluentValidation;

namespace Application.SupportChat.SaveMessageByDtoBus;

public class SaveMessageByDtoBusValidator : AbstractValidator<SaveMessageByDtoBusCommand>
{
    public SaveMessageByDtoBusValidator()
    {
        RuleFor(c => c.Message).NotNull();
    }
}