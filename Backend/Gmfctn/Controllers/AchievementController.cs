﻿using AutoMapper;
using Data_;
using Data_.Dtos;
using Data_.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gmfctn.Controllers
{
    [Route("api/achievement")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public AchievementController(GmfctnContext context, IMapper mapper) {
            unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievement>>> GetAllAchievement()
        {
            IEnumerable<Achievement> Items = null;
            try
            {
                Items = await unitOfWork.AchievementRepository.GetAll();
                if (Items == null)
                    return NotFound();
                else
                    return Ok(Items);
            }
            catch(Exception exc) {
                throw exc;
            }
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Achievement>> GetAchivementById(Guid id)
        {
            try {
                var Item = await unitOfWork.AchievementRepository.GetById(id);
                if (Item != null)
                {
                    return Ok(Item);
                }
                return NotFound();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateAchievement(AchievementCreateDTO element)
        {
            try
            {
                var achievement = _mapper.Map<Achievement>(element);
                achievement.Id = new Guid();
                await unitOfWork.AchievementRepository.Create(achievement);
                await unitOfWork.AchievementRepository.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException exc)
            {
                throw new ArgumentException();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAchievement(Guid id)
        {
            try
            {
                await unitOfWork.AchievementRepository.Delete(id);
                await unitOfWork.AchievementRepository.SaveChanges();
                return Ok();
            }
            catch (ArgumentException exc)
            {
                return NotFound();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAchievement(Guid id, AchievementUpdateDTO achievement)
        {
            try
            {
                var result = await unitOfWork.AchievementRepository.dbSet.FirstOrDefaultAsync(item => item.Id == id);
                _mapper.Map(achievement, result);
                await unitOfWork.AchievementRepository.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException exc)
            {
                throw new ArgumentException();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
