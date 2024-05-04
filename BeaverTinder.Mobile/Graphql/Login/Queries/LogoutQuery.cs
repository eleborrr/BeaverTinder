using BeaverTinder.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Mobile.Graphql.Login.Queries;

public class LogoutQuery
{
    private readonly SignInManager<User> _signInManager;
    
    public LogoutQuery(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }
    
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}