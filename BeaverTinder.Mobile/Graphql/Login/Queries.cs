using BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Shared.Dto.Subscription;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Mobile.Graphql.Queries;

public partial class Queries
{
    public async Task<string> Logout()
    {
        return await Task.FromResult("logout success");
    }
}
