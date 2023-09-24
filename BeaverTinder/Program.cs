 using System.Security.Claims;
 using System.Text;
 using Contracts.Configs;
 using Domain.Entities;
 using Domain.Repositories;
 using Microsoft.AspNetCore.Authentication.JwtBearer;
 using Microsoft.AspNetCore.Identity;
 using Microsoft.EntityFrameworkCore;
 using Microsoft.IdentityModel.Tokens;
 using Microsoft.OpenApi.Models;
 using Persistence;
 using Persistence.Misc.Services.JwtGenerator;
 using Persistence.Repositories;
 using Presentation.Hubs;
 using Services;
 using Services.Abstraction;

 var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
 builder.Services.AddControllers();
 builder.Services.AddMemoryCache();
 builder.Services.AddSignalR();

 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddMvc();

 builder.Services.AddDbContext<ApplicationDbContext>(options =>
 {
     options.UseSqlServer(builder.Configuration.GetConnectionString("BeaverTinderDatabase"));
     options.EnableSensitiveDataLogging();
 });
 builder.Services.AddIdentity<User, Role>(
         options =>
         {
             options.SignIn.RequireConfirmedAccount = false; // change in prod
             options.SignIn.RequireConfirmedEmail = true;  // change in prod
         })
     .AddDefaultTokenProviders()
     .AddEntityFrameworkStores<ApplicationDbContext>();
 builder.Services.Configure<DataProtectionTokenProviderOptions>(
     o => o.TokenLifespan = TimeSpan.FromHours(24));



 builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
 builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
 builder.Services.AddScoped<IServiceManager , ServiceManager>();
 builder.Services.AddScoped<HttpClient>();
 builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("SmtpSettings"));
 
 builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
 
 builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters()
         {
             ValidateIssuer = true,
             ValidIssuer = builder.Configuration["JWTTokenSettings:ISSUER"],
             ValidateAudience = true,
             ValidAudience = builder.Configuration["JWTTokenSettings:AUDIENCE"],
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(builder.Configuration["JWTTokenSettings:KEY"]!))
         };
     });
     

 builder.Services.AddAuthorization(options =>
 {
     options.AddPolicy("OnlyMapSubs", policy =>
     {
         policy.RequireClaim(ClaimTypes.Role, "UserMoreLikesAndMap", "Moderator", "Admin");
     });
     options.AddPolicy("OnlyLikeSubs", policy =>
     {
         policy.RequireClaim(ClaimTypes.Role, "UserMoreLikes", "Moderator", "Admin");
     });
     options.AddPolicy("OnlyForAdmins", policy => {
         policy.RequireClaim(ClaimTypes.Role, "Admin");
     });
     options.AddPolicy("OnlyForModerators", policy => {
         policy.RequireClaim(ClaimTypes.Role, "Moderator", "Admin");
         
     });
 });

 builder.Services.AddSwaggerGen(opt =>
 {
     opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
     opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
         In = ParameterLocation.Header,
         Description = "Please enter token",
         Name = "Authorization",
         Type = SecuritySchemeType.Http,
         BearerFormat = "JWT",
         Scheme = "bearer"
     });
     opt.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type=ReferenceType.SecurityScheme,
                     Id="Bearer"
                 }
             },
             Array.Empty<string>()
         }
     });
 });
 
 const string testSpesific = "testSpesific";

 builder.Services.AddRouting(options =>
 {
     options.LowercaseUrls = true;
     options.LowercaseQueryStrings = false;
 });

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 app.MapHub<ChatHub>("/chatHub");

 app.UseCors(testSpesific);

 app.UseHttpsRedirection();

 app.UseAuthentication(); 
 app.UseAuthorization();

 app.MapControllers();

app.Run();