using Data_.Entities;
using Data_.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gmfctn.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJwtAuthenticationService JwtAuthenticationService;
        public AuthController(IJwtAuthenticationService _JwtAuthenticationService)
        {
            JwtAuthenticationService = _JwtAuthenticationService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<List<string>>> AuthenticateWithUserName(string Login, string Password, CancellationToken Cancel)
        {
            var Tokens = await JwtAuthenticationService.Authenticate(Login, Password, Cancel);
            if (Tokens.Item1 == null || Tokens.Item2 == null)
                return Unauthorized();

            return Ok(new List<string> { Tokens.Item1, Tokens.Item2 });
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<List<string>>> Refresh( string Token, string RefreshToken, CancellationToken Cancel)
        {
            var Tokens = await JwtAuthenticationService.RefreshToken(Token, RefreshToken, Cancel);
            if(Tokens.Item1 == null || Tokens.Item2 == null)
                return BadRequest();

            return Ok(new List<string> { Tokens.Item1, Tokens.Item2 });
        }

    }
}

