namespace BeaverTinder.Mobile.ServicesExtensions.GraphQL;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddGraphQLServer();
            // .AddQueryType<TeacherQuery>()
            // .AddMutationType<TeacherMutation>()
            // .AddSubscriptionType<TeacherSubscription>();

        return services;
    }
    
}