﻿using Microsoft.AspNetCore.Identity;

namespace NIGWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
