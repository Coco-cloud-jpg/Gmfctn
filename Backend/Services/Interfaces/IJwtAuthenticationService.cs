using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IJwtAuthenticationService
    {
        Task<(string, string, string)>  Authenticate(string Login, string Password, CancellationToken Cancel, bool isHashed = false);
        Task<(string, string, string)> RefreshToken(string Token, string RefreshToken, CancellationToken Cancel);
    }
}
