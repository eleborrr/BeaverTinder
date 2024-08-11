using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Domain.Entities;

public class Role: IdentityRole
{
    public int LikesCountAllowed { get; set; }
    public bool LocationViewAllowed { get; set; }
    
}