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

namespace Gmfctn.Controllers
{
    [Route("api/achievement")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private IUnitOfWork UnitOfWork;
        private readonly IMapper _Mapper;
        public AchievementController(GmfctnContext Context, IMapper Mapper) {
            UnitOfWork = new UnitOfWork(Context);
            _Mapper = Mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAllAchievement(CancellationToken Cancel)
        {
            try
            {
                var Achievements = await UnitOfWork.AchievementRepository.GetAll(Cancel);
                if (Achievements == null)
                    return NotFound();
                else
                    return Ok(Achievements);
            }
            catch(Exception Exc) {
                throw Exc;
            }
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Achievement>> GetAchivementById(Guid Id, CancellationToken Cancel)
        {
            try {
                var Achievement = await UnitOfWork.AchievementRepository.GetById(Id, Cancel);
                if (Achievement != null)
                {
                    return Ok(Achievement);
                }
                return NotFound();
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
                if (!ModelsValidator.AchievementIsValid(Achievement))
                    ModelState.AddModelError("achievement", "Contains errors in parametrs.");
                if (ModelState.IsValid) 
                {
                    var _Achievement = _Mapper.Map<Achievement>(Achievement);
                    _Achievement.Id = new Guid();
                    await UnitOfWork.AchievementRepository.Create(_Achievement, Cancel);
                    await UnitOfWork.SaveChangesAsync(Cancel);
                    return Ok();
                }
                return BadRequest(ModelState);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAchievement(Guid Id, CancellationToken Cancel)
        {
            try
            {
                await UnitOfWork.AchievementRepository.Delete(Id, Cancel);
                await UnitOfWork.SaveChangesAsync(Cancel);
                return Ok();
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
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAchievement(Guid Id, AchievementUpdateDTO Achievement, CancellationToken Cancel)
        {
            try
            {
                if (!ModelsValidator.AchievementIsValid(_Mapper.Map<AchievementCreateDTO>(Achievement)))
                    ModelState.AddModelError("achievement", "Contains errors in parametrs.");
                if (ModelState.IsValid)
                {
                    var _Achievement = await UnitOfWork.AchievementRepository.DbSet.FirstOrDefaultAsync(item => item.Id == Id);
                    _Mapper.Map(Achievement, _Achievement);
                    UnitOfWork.AchievementRepository.Update(_Achievement); 
                    await UnitOfWork.SaveChangesAsync(Cancel);
                    return Ok();
                }
                else 
                {
                    return BadRequest(ModelState);
                }
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
