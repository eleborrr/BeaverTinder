using System.Security.Claims;
using System.Text;
using BeaverTinder.Application.Services;
using BeaverTinder.Application.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BeaverTinder.Mobile.ServicesExtensions.CustomServices;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager , ServiceManager>();
        return services;
    }
    
    public static IServiceCollection AddCustomAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
        return services;
    }
}