using BeaverTinder.API.ServicesExtensions.Auth;
using BeaverTinder.API.ServicesExtensions.Grpc;
using BeaverTinder.API.ServicesExtensions.Services;
using BeaverTinder.Application.Helpers;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Infrastructure.Database;
using BeaverTinder.Infrastructure.Database.Repositories;
using BeaverTinder.SupportChat.Services;
using BeaverTinder.SupportChat.ServicesExtensions.RabbitMq;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpc();
builder.Services.ConfigureGrpc(builder.Configuration);

builder.Services.AddIdentity<User, Role>(
        options =>
        {
            options.SignIn.RequireConfirmedAccount = false; // change in prod
            options.SignIn.RequireConfirmedEmail = true;  // change in prod
        })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddCustomAuth(builder.Configuration);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddCustomServices(builder.Configuration);

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(AplicationAssemblyReference.Assembly);
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddScoped<ISupportChatMessageRepository, SupportChatMessageRepository>();
builder.Services.AddSingleton<ISupportChatRoomService, SupportChatRoomService>();
builder.Services.AddMasstransitRabbitMq(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<SupportChatRpcService>();

app.UseHttpsRedirection();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseAuthorization();

app.MapControllers();

app.Run();