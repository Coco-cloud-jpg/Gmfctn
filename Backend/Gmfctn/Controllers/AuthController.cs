using Data_.Entities;
using Data_.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Data_.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Gmfctn.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJwtAuthenticationService JwtAuthenticationService;
        private IMailService MailService;
        public AuthController(IJwtAuthenticationService _JwtAuthenticationService, IMailService _MailService)
        {
            JwtAuthenticationService = _JwtAuthenticationService;
            MailService = _MailService;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<List<string>>> AuthorizeWithLogin(Credentials Credentials, CancellationToken Cancel)
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
        [HttpPost("send-request")]
        public async Task<ActionResult> SendRequestToResetPassword(string Email, CancellationToken Cancel)
        {
            try
            {
                await MailService.SendRequest(Email, Cancel);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("reset")]
        public async Task<ActionResult> ResetPassword(PasswordResetData DataToReset, CancellationToken Cancel)
        {
            try
            {
                await JwtAuthenticationService.ResetPassword(DataToReset.NewPassword, DataToReset.Hash, Cancel);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

