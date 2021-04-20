using AutoMapper;
using Data_;
using Data_.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Gmfctn.Controllers
{
    [Route("api/achievement")]
    [ApiController]
    [Authorize]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementService AchievementService;

        public AchievementController(IAchievementService _AchievementService)
        {
            AchievementService = _AchievementService;
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAllAchievement(CancellationToken Cancel)
        {
            try
            {
                var Achievements = await AchievementService.GetAllAchievements(Cancel);
                if (Achievements == null)
                {
                    return NotFound();
                }
                return Ok(Achievements);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("{Id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Achievement>> GetAchivementById( Guid Id, CancellationToken Cancel)
        {
            try {
                var Achievement = await AchievementService.GetAchievementById(Id, Cancel);
                if (Achievement != null)
                {
                    return Ok(Achievement);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateAchievement(AchievementCreateDTO Achievement, CancellationToken Cancel)
        {
            try
            {
                await AchievementService.CreateAchievement(Achievement, Cancel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAchievement(Guid Id, CancellationToken Cancel)
        {
            try
            {
                await AchievementService.DeleteAchievement(Id, Cancel);
                return NoContent();
            }
            catch (ArgumentNullException ArgExp)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAchievement(Guid Id, AchievementUpdateDTO Achievement, CancellationToken Cancel)
        {
            try
            {
                await AchievementService.UpdateAchievement(Id, Achievement, Cancel);
                 return Ok();
            }
            catch (ArgumentNullException ArgExp)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
