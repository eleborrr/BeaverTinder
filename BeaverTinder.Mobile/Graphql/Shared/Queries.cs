namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    private readonly IServiceScopeFactory _scopeFactory;
    public Queries(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
}