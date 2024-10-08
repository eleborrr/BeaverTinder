﻿using BeaverTinder.Application.Features.OAuth.Login;
using FluentValidation;

namespace Application.OAuth.Login;

public class LoginOAuthVkValidator : AbstractValidator<LoginOAuthVkCommand>
{
    public LoginOAuthVkValidator()
    {
        RuleFor(c => c.SignedUser).NotNull();
    }
}