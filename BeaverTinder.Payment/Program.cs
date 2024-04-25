using BeaverTinder.Payment.Infrastructure.Persistence;
using BeaverTinder.Payment.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpc();
builder.Services.AddDbContext<PaymentDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase"));
    options.EnableSensitiveDataLogging();
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<PaymentRpcService>();

app.MapControllers();

app.Run();