using Data_.Dtos;
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
    [Route("api/request")]
    [ApiController]
    [Authorize]
    public class RequestAchievementController : ControllerBase
    {
        private IRequestAchievementService RequestAchievementService;
        public RequestAchievementController(IRequestAchievementService _RequestAchievementService)
        {
            RequestAchievementService = _RequestAchievementService;
        }
        [Authorize(Roles = "User")]
        [HttpPost("request")]
        public async Task<ActionResult> RequestAchievement(RequestBody RequestForAchievement, CancellationToken Cancel)
        {
            try
            {
                var AccessToken = Request.Headers[HeaderNames.Authorization].ToString().Substring("Bearer".Length + 1);
                await RequestAchievementService
                    .AddRequest(AccessToken, RequestForAchievement.Message, RequestForAchievement.AchievementId, Cancel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("decline")]
        public async Task<ActionResult> DeclineAchievementRequest(Guid RequestForAchievementId, CancellationToken Cancel)
        {
            try
            {
                await RequestAchievementService.DeclineRequest(RequestForAchievementId, Cancel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("accept")]
        public async Task<ActionResult> AcceptAchievementRequest(Guid RequestForAchievementId, CancellationToken Cancel)
        {
            try
            {
                await RequestAchievementService.AcceptRequest(RequestForAchievementId, Cancel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<RequestAchievement>>> GetAchievementRequests( CancellationToken Cancel)
        {
            try
            {
                var AchievementRequests = await RequestAchievementService.GetAllRequestAchievements(Cancel);
                return Ok(AchievementRequests);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

