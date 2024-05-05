
using BeaverTinder.API.ServicesExtensions.Auth;
using BeaverTinder.API.ServicesExtensions.Services;
using BeaverTinder.Application.Helpers;
using BeaverTinder.Mobile.ServicesExtensions.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL(builder.Configuration, builder.Environment);
builder.Services.AddCustomAuth(builder.Configuration);
builder.Services.AddCustomServices(builder.Configuration);

// builder.Services.AddMediatR(configuration =>
// {
//     configuration.RegisterServicesFromAssembly(AplicationAssemblyReference.Assembly);
//     configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
// });


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.MapGraphQL((PathString)"/graphql");

app.Run();