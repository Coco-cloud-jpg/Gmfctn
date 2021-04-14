using AutoMapper;
using Data_;
using Data_.Dtos;
using Data_.Interfaces;
using Data_.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services;
using Services.Interfaces;

namespace Gmfctn.Controllers
{
    [Route("api/achievement")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IMapper Mapper;

        private readonly IAchievementService AchievementService;

        public AchievementController(IAchievementService _AchievementService, IMapper _Mapper)
        {
            AchievementService = _AchievementService;
            Mapper = _Mapper;
        }
        [HttpGet]
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
            catch (TaskCanceledException Exc)
            {
                return Ok(Exc.Data);
            }
            catch (Exception Exc) {
                throw Exc;
            }
            
        }
        [HttpGet("{Id}")]
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
            catch (TaskCanceledException Exc)
            {
                return Ok(Exc.Data);
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateAchievement(AchievementCreateDTO Achievement, CancellationToken Cancel)
        {
            try
            {
                if (await AchievementService.CreateAchievement(Achievement, Cancel))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (TaskCanceledException Exc)
            {
                return Ok(Exc.Data);
            }
            catch (DbUpdateException Exc)
            {
                throw new ArgumentException();
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAchievement(Guid Id, CancellationToken Cancel)
        {
            try
            {
                if (await AchievementService.DeleteAchievement(Id, Cancel))
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (TaskCanceledException Exc)
            {
                return Ok(Exc.Data);
            }
            catch (ArgumentException Exc)
            {
                return NotFound();
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAchievement(Guid Id, AchievementUpdateDTO Achievement, CancellationToken Cancel)
        {
            try
            {
                if (await AchievementService.UpdateAchievement(Id, Achievement, Cancel))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (TaskCanceledException Exc)
            {
                return Ok(Exc.Data);
            }
            catch (DbUpdateException Exc)
            {
                throw new ArgumentException();
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
    }
}
