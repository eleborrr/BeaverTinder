namespace BeaverTinder.Mobile.Graphql.Queries;

public partial class Queries
{
    private readonly IServiceScopeFactory _scopeFactory;
    public Queries(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
}