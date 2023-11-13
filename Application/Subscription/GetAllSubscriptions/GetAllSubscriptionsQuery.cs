using Services.Abstraction.Cqrs.Queries;

namespace Application.Subscription.GetAllSubscriptions;

public class GetAllSubscriptionsQuery: IQuery<IEnumerable<Domain.Entities.Subscription>>
{
    
}