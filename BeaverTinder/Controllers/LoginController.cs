﻿using Microsoft.AspNetCore.Authorization;

namespace BeaverTinder.Controllers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

public class LoginController
{
    [Authorize]
    public void Login()
    {
        var inputLogin = ""; //
        var inputPassword = ""; // 
    }
}