namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Mutations
{
    private readonly IServiceScopeFactory _scopeFactory;
    public Mutations(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
}