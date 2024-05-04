using System.Security.Claims;
using BeaverTinder.Mobile.Helpers;
using BeaverTinder.Mobile.Helpers.Filters;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Authorization;

namespace BeaverTinder.Mobile.ServicesExtensions.GraphQL;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services
            .AddGraphQLServer()
            .AddErrorFilter(provider =>
            {
                return new ServerErrorFilter(
                    provider.GetRequiredService<ILogger<ServerErrorFilter>>(),
                    environment);
            })
            .AddAuthorization(options => 
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
            })

            // .AddQueryType<TeacherQuery>()
            // .AddMutationType<TeacherMutation>()
            // .AddSubscriptionType<TeacherSubscription>();
            ;
        return services;
    }
    
}