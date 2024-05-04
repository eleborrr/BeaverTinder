using BeaverTinder.Mobile.ServicesExtensions.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.MapGraphQL((PathString)"/graphql");

app.Run();