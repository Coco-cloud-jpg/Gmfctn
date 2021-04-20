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
        public async Task<ActionResult<List<string>>> AuthenticateWithUserName(Credentials Credentials, CancellationToken Cancel)
        {
            try
            {
                var Tokens = await JwtAuthenticationService.Authenticate(Credentials.Login, Credentials.Password, Cancel);
                if (Tokens.Item1 == null || Tokens.Item2 == null || Tokens.Item3 == null)
                    return Unauthorized();

                return Ok(new List<string> { Tokens.Item1, Tokens.Item2, Tokens.Item3 });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<List<string>>> Refresh(Tokens Tokens, CancellationToken Cancel)
        {
            try
            {
                var newTokens = await JwtAuthenticationService.RefreshToken(Tokens.AccessToken, Tokens.RefreshToken, Cancel);
                if (newTokens.Item1 == null || newTokens.Item2 == null || newTokens.Item3 == null)
                    return BadRequest();

                return Ok(new List<string> { newTokens.Item1, newTokens.Item2, newTokens.Item3 });
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}

