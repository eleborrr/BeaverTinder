using BeaverTinder.API.ServicesExtensions.Auth;
using BeaverTinder.API.ServicesExtensions.Grpc;
using BeaverTinder.API.ServicesExtensions.MassTransit;
using BeaverTinder.API.ServicesExtensions.SecurityAndCors;
using BeaverTinder.API.ServicesExtensions.Services;
using BeaverTinder.Application.Helpers;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Infrastructure.Database;
using BeaverTinder.Mobile.ServicesExtensions.GraphQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddGraphQL(builder.Configuration, builder.Environment);
builder.Services.AddIdentity<User, Role>(
     options =>
     {
         options.SignIn.RequireConfirmedAccount = false; // change in prod
         options.SignIn.RequireConfirmedEmail = true;  // change in prod
     })
 .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddCustomAuth(builder.Configuration);
builder.Services.Configure<DataProtectionTokenProviderOptions>(
 o => o.TokenLifespan = TimeSpan.FromHours(24));


builder.Services.ConfigureGrpc(builder.Configuration);

builder.Services.AddCustomServices(builder.Configuration);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(AplicationAssemblyReference.Assembly);
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

const string testSpesific = "testSpesific";

builder.Services.AddCustomCors(testSpesific);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
 options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase"));
 options.EnableSensitiveDataLogging();
});

builder.Services.AddMasstransitRabbitMq(builder.Configuration);

var app = builder.Build();

app.UseCors(testSpesific);

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL(new PathString("/graphql"));

app.Run();
