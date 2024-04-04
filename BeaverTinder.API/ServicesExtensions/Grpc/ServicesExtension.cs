using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BeaverTinder.API.ServicesExtensions.Grpc;

public static class ServicesCollectionExtension
{
    public static IServiceCollection ConfigureGrpc(this IServiceCollection services)
    {
        services.AddGrpc();
        var client = services.AddGrpcClient<Payments.PaymentsClient>(o => o.Address = new Uri(builder.Configuration.GetSection("Grpc")["PaymentsAddress"]));

        return services;
    }
}