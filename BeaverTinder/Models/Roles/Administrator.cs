using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Models;

/// <summary>
/// Администратор владеет всеми правами, может банить всех, удалять все сообщения
/// удалять пользователей, в том числе модераторов
/// </summary>

public class Administrator: IdentityRole
{
    
}