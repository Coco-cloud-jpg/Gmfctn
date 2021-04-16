using Data_.Entities;
using Data_.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gmfctn.Controllers
{
    [Route("api/thank")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ThankController : ControllerBase
    {
        private readonly IThankService ThankService;
        public ThankController(IThankService _ThankService)
        {
            ThankService = _ThankService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> SayThank(string Text, Guid ToUserId, CancellationToken Cancel)
        {
           var AccessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer".Length + 1);
            if (await ThankService.SayThank(AccessToken, Text, ToUserId, Cancel))
                return Ok();
            return BadRequest();
        }
        [HttpGet]
        public async Task<ActionResult<Thank>> GetThank( CancellationToken Cancel)
        {
            var AccessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer".Length + 1);
            return await ThankService.GetThank(AccessToken, Cancel);
        }

    }
}

