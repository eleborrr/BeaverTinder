var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL();

var app = builder.Build();

app.UseWebSockets();
app.MapGraphQL((PathString)"/graphql");

app.Run();