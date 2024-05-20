using System.Security.Claims;
using BeaverTinder.Mobile.Graphql.Shared;
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
            .AddAuthorization()
            // .AddSubscriptionType<TeacherSubscription>();
            ;
        return services;
    }
    
}