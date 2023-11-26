using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Infrastructure.Database;
using BeaverTinder.Infrastructure.Database.Repositories;
using BeaverTinder.SupportChat.ServicesExtensions.RabbitMq;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase"));
    options.EnableSensitiveDataLogging();
});
builder.Services.AddScoped<ISupportChatMessageRepository, SupportChatMessageRepository>();

builder.Services.AddMasstransitRabbitMq(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();