﻿using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BeaverTinder.API.ServicesExtensions.Auth;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddCustomAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTTokenSettings:ISSUER"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTTokenSettings:AUDIENCE"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWTTokenSettings:KEY"]!))
                };
            });
        services.AddAuthorizationBuilder();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("OnlyMapSubs", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "UserMoreLikesAndMap", "Moderator", "Admin");
            });
            options.AddPolicy("OnlyLikeSubs", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "UserMoreLikes", "Moderator", "Admin");
            });
            options.AddPolicy("OnlyForAdmins", policy => {
                policy.RequireClaim(ClaimTypes.Role, "Admin");
            });
            options.AddPolicy("OnlyForModerators", policy => {
                policy.RequireClaim(ClaimTypes.Role, "Moderator", "Admin");
     
            });
        }).ConfigureApplicationCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.HttpOnly = true;
        });
        return services;
    }
}