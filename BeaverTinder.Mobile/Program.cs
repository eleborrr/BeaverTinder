using BeaverTinder.Mobile.ServicesExtensions.CustomServices;
using BeaverTinder.Mobile.ServicesExtensions.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL(builder.Configuration, builder.Environment);
builder.Services.AddCustomServices();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.MapGraphQL((PathString)"/graphql");

app.Run();