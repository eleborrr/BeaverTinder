﻿namespace BeaverTinder.API.ServicesExtensions.SecurityAndCors;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, string testSpesific)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: testSpesific, policyBuilder =>
            {
                policyBuilder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
                policyBuilder.WithOrigins("https://oauth.vk.com")
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
                policyBuilder.WithOrigins("https://localhost:7015")
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
                policyBuilder.WithOrigins("http://localhost:51319");
            });

        });
        return services;
    }
}