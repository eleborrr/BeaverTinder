using BeaverTinder.S3.Services;
using BeaverTinder.S3.ServicesExtensions.RabbitMq;
using BeaverTinder.S3.ServicesExtensions.S3;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FileGetterService>();
builder.Services.AddS3Client(builder.Configuration);
builder.Services.AddMasstransitRabbitMq(builder.Configuration);


var testSpesific = "testSpesific";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: testSpesific, policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
        policyBuilder.WithOrigins("https://oauth.vk.com")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
        policyBuilder.WithOrigins("https://localhost:7015")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
    });

});

var app = builder.Build();

app.MapGet("api/files/{bucketName}", async ([FromQuery] string fileName, [FromServices] FileGetterService storageService) =>
{
    var bytes = await storageService.GetFiles(fileName);
    return bytes is not null
        ? Results.Ok(bytes)
        : Results.NotFound();
});

app.UseCors(testSpesific);

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