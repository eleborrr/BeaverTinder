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

var app = builder.Build();

app.MapGet("api/files/{bucketName}/{fileName}", async (string[] fileNames, [FromServices] FileGetterService storageService) =>
{
    var stream = await storageService.GetFiles(fileNames);

    return stream is not null
        ? Results.Ok(stream)
        : Results.NotFound();
});

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