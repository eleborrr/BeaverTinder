using BeaverTinder.API.Hubs;
using BeaverTinder.API.ServicesExtensions.Auth;
using BeaverTinder.API.ServicesExtensions.MassTransit;
using BeaverTinder.API.ServicesExtensions.SecurityAndCors;
using BeaverTinder.API.ServicesExtensions.Services;
using BeaverTinder.API.ServicesExtensions.Swagger;
using BeaverTinder.Application.Helpers;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Configuration.AddEnvironmentVariables();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();

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

builder.Services.AddRouting(options =>
{
 options.LowercaseUrls = true;
 options.LowercaseQueryStrings = false;
});

builder.Services.AddCustomCors(testSpesific);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<ChatHub>("/chatHub");
app.MapHub<SupportChatHub>("/supportChatHub");

app.UseCors(testSpesific);

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();