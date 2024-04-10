﻿using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BeaverTinder.API.ServicesExtensions.Grpc;

public static class ServicesCollectionExtension
{
    public static IServiceCollection ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();
        services.AddGrpcClient<grpcServices.Payment.PaymentClient>
            (o => o.Address = new Uri(configuration.GetSection("Grpc")["PaymentsAddress"]));
        services.AddGrpcClient<grpcServices.Subscription.SubscriptionClient>
            (o => o.Address = new Uri(configuration.GetSection("Grpc")["SubscriptionAddress"]));
        return services;
    }
}