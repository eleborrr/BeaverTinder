using System.Security.Claims;
using BeaverTinder.Mobile.Graphql.Mutations;
using BeaverTinder.Mobile.Graphql.Queries;
using BeaverTinder.Mobile.Helpers.Filters;
using BeaverTinder.Mobile.Helpers.PolicyStrings;
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
                options.AddPolicy(PolicyStaticStrings.MapSubs, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "UserMoreLikesAndMap", "Moderator", "Admin");
                });
                options.AddPolicy(PolicyStaticStrings.LikeSubs, policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "UserMoreLikes", "Moderator", "Admin");
                });
                options.AddPolicy(PolicyStaticStrings.ForAdmins, policy => {
                    policy.RequireClaim(ClaimTypes.Role, "Admin");
                });
                options.AddPolicy(PolicyStaticStrings.ForModerators, policy => {
                    policy.RequireClaim(ClaimTypes.Role, "Moderator", "Admin");
     
                });
            })
            // .AddSubscriptionType<TeacherSubscription>();
            ;
        return services;
    }
    
}