using System.Security.Claims;
using BeaverTinder.Mobile.Graphql.Login.Queries;
using BeaverTinder.Mobile.Helpers.Filters;

namespace BeaverTinder.Mobile.ServicesExtensions.GraphQL;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        services
            .AddGraphQLServer()
            .AddMutationType<Mutations>()
            .AddQueryType<Queries>()
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
            // .AddSubscriptionType<TeacherSubscription>();
            ;
        return services;
    }
    
}