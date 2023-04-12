 using System.Security.Claims;
 using BeaverTinder.DataBase;
 using BeaverTinder.Models;
 using BeaverTinder.Services;
 using BeaverTinder.Services.Pay;
 using DogApi.Models;
 using DogApi.Services;
 using Microsoft.AspNetCore.Authentication.Cookies;
 using Microsoft.AspNetCore.Identity;
 using Microsoft.EntityFrameworkCore;

 var TestSpesific = "testSpesific";

 var builder = WebApplication.CreateBuilder(args);


 builder.Services.ConfigureApplicationCookie(options =>
 {
     if (builder.Environment.IsDevelopment())
     {
         options.Cookie.SameSite = SameSiteMode.None;
         options.Cookie.HttpOnly = true;
         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
     }
 });
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddMvc();
 builder.Services.AddDbContext<dbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase")));
 builder.Services.AddIdentity<User, IdentityRole>(
         options =>
         {
             options.SignIn.RequireConfirmedAccount = false; // change in prod
             options.SignIn.RequireConfirmedEmail = false;  // change in prod
         })
     .AddDefaultTokenProviders()
     .AddEntityFrameworkStores<dbContext>();
 builder.Services.Configure<DataProtectionTokenProviderOptions>(
     o => o.TokenLifespan = TimeSpan.FromHours(3));
 builder.Services.AddScoped<ITwoFAService ,TwoFAService>();
 builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("SmtpSettings"));
 builder.Services.AddScoped<IEmailServiceInterface, EmailService>();
 builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options =>
     {
         options.LoginPath = "/login";
         options.AccessDeniedPath = "../login";
     });
 builder.Services.AddAuthorization(options =>
 {
     options.AddPolicy("OnlyMapSubs", policy =>
     {
         policy.RequireClaim("Subscription", "Map");
     });
     options.AddPolicy("OnlyLikeSubs", policy =>
     {
         policy.RequireClaim("Subscription", "Like");
     });
     options.AddPolicy("OnlyForAdmins", policy => {
         policy.RequireClaim(ClaimTypes.Role, "Admin");
     });
     options.AddPolicy("OnlyForModerators", policy => {
         policy.RequireClaim(ClaimTypes.Role, "Moderator");
     });
 });
 builder.Services.AddRouting(options =>
 {
     options.LowercaseUrls = true;
     options.LowercaseQueryStrings = false;
 });

 builder.Services.AddCors(options =>
 {
     options.AddPolicy(name: TestSpesific, policyBuilder =>
     {
         policyBuilder.WithOrigins("http://localhost:3000")
             .AllowAnyHeader()
             .AllowCredentials()
             .AllowAnyMethod();
     });
 });

 var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

 app.UseCors(TestSpesific);

app.UseHttpsRedirection();
 
 app.UseAuthentication(); 
 app.UseAuthorization();

 

 app.MapControllers();

app.Run();