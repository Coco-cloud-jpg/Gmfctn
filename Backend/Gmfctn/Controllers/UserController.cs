using AutoMapper;
using Data_;
using Data_.Dtos;
using Data_.Entities;
using Data_.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmfctn.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public UserController(GmfctnContext context, IMapper mapper)
        {
            unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllAchievement()
        {
            IEnumerable<User> Items = null;
            try
            {
                Items = await unitOfWork.UserRepository.GetAll();
                if (Items == null)
                    return NotFound();
                else
                    return Ok(Items);
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetAchivementById(Guid id)
        {
            try
            {
                var Item = await unitOfWork.UserRepository.GetById(id);
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
        public async Task<ActionResult> CreateUser(UserCreateDTO element)
        {
            try
            {
                var achievement = _mapper.Map<User>(element);
                achievement.Id = new Guid();
                await unitOfWork.UserRepository.Create(achievement);
                await unitOfWork.UserRepository.SaveChanges();
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
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                await unitOfWork.UserRepository.Delete(id);
                await unitOfWork.UserRepository.SaveChanges();
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
        public async Task<ActionResult> UpdateUser(Guid id, UserUpdateDTO achievement)
        {
            try
            {
                var result = await unitOfWork.UserRepository.dbSet.FirstOrDefaultAsync(item => item.Id == id);
                _mapper.Map(achievement, result);
                await unitOfWork.UserRepository.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException exc)
            {
                throw new ArgumentException(exc.Message);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
