using BeaverTinder.Subscription.Core.Abstractions.Repositories;
using BeaverTinder.Subscription.Infrastructure.Persistence;
using BeaverTinder.Subscription.Infrastructure.Repositories;
using BeaverTinder.Subscription.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddDbContext<SubscriptionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase"));
    options.EnableSensitiveDataLogging();
});
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<SubscriptionRpcService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();