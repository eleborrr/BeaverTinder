﻿using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Account;
using Services.Abstraction.Email;
using Services.Abstraction.FindBeaver;
using Services.Abstraction.Geolocation;
using Services.Abstraction.Likes;
using Services.Abstraction.TwoFA;

namespace Services.Abstraction;

public interface IServiceManager
{
    IEmailService EmailService { get; }
    ITwoFAService TwoFaService { get; }
    ILikeService LikeService { get; }
    IGeolocationService GeolocationService { get; }
    IFindBeaverService FindBeaverService { get; }
    IAccountService AccountService { get; }
}