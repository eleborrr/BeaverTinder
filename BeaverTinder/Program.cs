 using System.Security.Claims;
 using Contracts.Configs;
 using Domain.Entities;
 using Domain.Repositories;
 using Microsoft.AspNetCore.Authentication.Cookies;
 using Microsoft.AspNetCore.Identity;
 using Microsoft.EntityFrameworkCore;
 using Persistence;
 using Persistence.Repositories;
 using Services;
 using Services.Abstraction;
 using Services.Abstraction.TwoFA;

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
 builder.Services.AddMemoryCache();
 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddMvc(); 
 builder.Services.ConfigureApplicationCookie(options =>
 {
     if (builder.Environment.IsDevelopment())
     {
         options.Cookie.SameSite = SameSiteMode.None;
         options.Cookie.HttpOnly = true;
         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
     }
 });

 builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase")));
 builder.Services.AddIdentity<User, IdentityRole>(
         options =>
         {
             options.SignIn.RequireConfirmedAccount = false; // change in prod
             options.SignIn.RequireConfirmedEmail = false;  // change in prod
         })
     .AddDefaultTokenProviders()
     .AddEntityFrameworkStores<ApplicationDbContext>();
 builder.Services.Configure<DataProtectionTokenProviderOptions>(
     o => o.TokenLifespan = TimeSpan.FromHours(3));
 
 builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
 builder.Services.AddScoped<IServiceManager , ServiceManager>();
 builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("SmtpSettings"));
 
 builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
 
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
 
 var TestSpesific = "testSpesific";

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

 app.UseCors(TestSpesific);

app.UseHttpsRedirection();
 
 app.UseAuthentication(); 
 app.UseAuthorization();

 

 app.MapControllers();

app.Run();