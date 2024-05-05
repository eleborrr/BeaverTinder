namespace BeaverTinder.Mobile.Graphql.Mutations;

public partial class Mutations
{
    private readonly IServiceScopeFactory _scopeFactory;
    public Mutations(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
}