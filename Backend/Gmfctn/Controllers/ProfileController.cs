using Data_;
using Data_.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Services.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gmfctn.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService ProfileService;

        public ProfileController(IProfileService _ProfileService)
        {
            ProfileService = _ProfileService;
        }

        [HttpGet("user_info")]
        public async Task<ActionResult<UserReadDTO>> GetCurrentUser(CancellationToken Cancel)
        {
            try
            {
                var AccessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer".Length + 1);
                return await ProfileService.GetCurrentUser(AccessToken, Cancel);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("user_achievements")]
        public async Task<ActionResult<ICollection<Achievement>>> GetCurrentUserAchievements(CancellationToken Cancel)
        {
            try
            {
                var AccessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer".Length + 1);
                var Achievements = await ProfileService.GetCurrentUserAchievements(AccessToken, Cancel);
                if (Achievements == null)
                    return NoContent();
                return Ok(Achievements);
            }
            catch
            {
                return  BadRequest();
            }
        }

    }
}
