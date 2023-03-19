 using System.Security.Claims;
 using BeaverTinder.DataBase;
 using Microsoft.AspNetCore.Authentication.Cookies;
 using Microsoft.EntityFrameworkCore;

 var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase"))); 
 builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options =>
     {
         options.LoginPath = "/login";
         options.AccessDeniedPath = "/login";
     }); 
 
 builder.Services.AddAuthorization(opts => {
 
     opts.AddPolicy("OnlyForAdmins", policy => {
         policy.RequireClaim(ClaimTypes.Role, "Admin");
     });
     opts.AddPolicy("OnlyForModerators", policy => {
         policy.RequireClaim(ClaimTypes.Role, "Moderator");
     });
 });
 
 
    

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