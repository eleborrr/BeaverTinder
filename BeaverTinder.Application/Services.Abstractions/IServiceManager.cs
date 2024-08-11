using BeaverTinder.Application.Services.Abstractions.Account;
using BeaverTinder.Application.Services.Abstractions.Chat;
using BeaverTinder.Application.Services.Abstractions.Email;
using BeaverTinder.Application.Services.Abstractions.FindBeaver;
using BeaverTinder.Application.Services.Abstractions.Geolocation;
using BeaverTinder.Application.Services.Abstractions.Likes;
using BeaverTinder.Application.Services.Abstractions.OAuth;
using BeaverTinder.Application.Services.Abstractions.SupportChat;
using BeaverTinder.Application.Services.Abstractions.TwoFA;
using BeaverTinder.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Services.Abstractions;

public interface IServiceManager
{
    UserManager<User> UserManager { get; }
    IEmailService EmailService { get; }
    ITwoFaService TwoFaService { get; }
    ILikeService LikeService { get; }
    IGeolocationService GeolocationService { get; }
    IFindBeaverService FindBeaverService { get; }
    IAccountService AccountService { get; }
    IVkOAuthService VkOAuthService { get; }
    IChatService ChatService { get; }
    ISupportChatService SupportChatService { get; }
}