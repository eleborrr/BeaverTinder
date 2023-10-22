using Contracts.Configs;
using SupportChat.Data;
using SupportChat.Services;
using SupportChat.ServicesExtensions.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddEnvironmentVariables();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>();
// builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMQCSettings"));
builder.Services.AddScoped<SupportChatConsumer>();
builder.Services.AddMasstransitRabbitMq(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();