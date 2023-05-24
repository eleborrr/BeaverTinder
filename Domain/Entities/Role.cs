using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Role: IdentityRole
{
    public int LikesCountAllowed { get; set; }
    public bool LocationViewAllowed { get; set; }
    
}