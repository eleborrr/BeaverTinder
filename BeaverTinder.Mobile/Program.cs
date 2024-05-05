using BeaverTinder.API.ServicesExtensions.Auth;
using BeaverTinder.API.ServicesExtensions.Grpc;
using BeaverTinder.API.ServicesExtensions.MassTransit;
using BeaverTinder.API.ServicesExtensions.SecurityAndCors;
using BeaverTinder.API.ServicesExtensions.Services;
using BeaverTinder.API.ServicesExtensions.Swagger;
using BeaverTinder.Application.Helpers;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Infrastructure.Database;
using BeaverTinder.Mobile.ServicesExtensions.GraphQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
builder.Services.AddGraphQL(builder.Configuration, builder.Environment);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
 options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase"));
 options.EnableSensitiveDataLogging();
});
builder.Services.AddIdentity<User, Role>(
     options =>
     {
         options.SignIn.RequireConfirmedAccount = false; // change in prod
         options.SignIn.RequireConfirmedEmail = true;  // change in prod
     })
 .AddDefaultTokenProviders()
 .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<DataProtectionTokenProviderOptions>(
 o => o.TokenLifespan = TimeSpan.FromHours(24));


builder.Services.ConfigureGrpc(builder.Configuration);

builder.Services.AddCustomServices(builder.Configuration);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(AplicationAssemblyReference.Assembly);
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCustomAuth(builder.Configuration);

builder.Services.AddMasstransitRabbitMq(builder.Configuration);


builder.Services.AddCustomSwaggerGenerator();
const string testSpesific = "testSpesific";

builder.Services.AddCustomCors(testSpesific);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(testSpesific);

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.MapGraphQL(new PathString("/graphql"));

app.Run();