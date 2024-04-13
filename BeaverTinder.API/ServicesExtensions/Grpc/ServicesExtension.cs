using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BeaverTinder.API.ServicesExtensions.Grpc;

public static class ServicesCollectionExtension
{
    public static IServiceCollection ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<BeaverTinder.Shared.Payment.PaymentClient>
            (o => o.Address = new Uri(configuration.GetSection("Grpc")["PaymentsAddress"]));
        services.AddGrpcClient<BeaverTinder.Shared.Subscription.SubscriptionClient>
            (o => o.Address = new Uri(configuration.GetSection("Grpc")["SubscriptionAddress"]));
        // services.AddGrpc();
        Console.WriteLine(configuration.GetSection("Grpc")["PaymentsAddress"]);
        Console.WriteLine(configuration.GetSection("Grpc")["SubscriptionAddress"]);
        Console.WriteLine("grpc clients configured");
        return services;
    }
}