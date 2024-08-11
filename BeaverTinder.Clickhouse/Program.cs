using BeaverTinder.Clickhouse.Helpers;
using BeaverTinder.Clickhouse.Repositories;
using BeaverTinder.Clickhouse.Repositories.Abstraction;
using BeaverTinder.Clickhouse.Services;
using BeaverTinder.Clickhouse.Services.Abstraction;
using BeaverTinder.Clickhouse.ServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.GetSection("Clickhouse").Bind(ConnectionStrings.Clickhouse);
builder.Services.AddScoped<IClickhouseService, ClickhouseService>();
builder.Services.AddScoped<IClickhouseRepository, ClickhouseRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMasstransitRabbitMq(builder.Configuration);
await MigrateClickhouse.UpsertDbAndTable();
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