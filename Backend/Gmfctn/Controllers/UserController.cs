using AutoMapper;
using Data_;
using Data_.Dtos;
using Data_.Entities;
using Data_.Interfaces;
using Data_.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Gmfctn.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWork UnitOfWork;
        private readonly IMapper _Mapper;
        
        public UserController(GmfctnContext Context, IMapper Mapper)
        {
            UnitOfWork = new UnitOfWork(Context);
            _Mapper = Mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers(CancellationToken Cancel)
        {
           
            try
            {
                var Users = await UnitOfWork.UserRepository.GetAll(Cancel);
                if (Users == null)
                    return NotFound();
                else
                    return Ok(Users);
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
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid Id, CancellationToken Cancel)
        {
            try
            {
                var User = await UnitOfWork.UserRepository.GetById(Id, Cancel);
                if (User != null)
                {
                    return Ok(User);
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
        public async Task<ActionResult> CreateUser(UserCreateDTO NewUser, CancellationToken Cancel)
        {
            try
            {
                if (!ModelsValidator.UserCreateIsValid(NewUser))
                    ModelState.AddModelError("user", "Contains errors in parametrs.");
                
                if (ModelState.IsValid)
                {
                    var User = _Mapper.Map<User>(NewUser);
                    User.Id = new Guid();
                    await UnitOfWork.UserRepository.Create(User, Cancel);
                    await UnitOfWork.SaveChangesAsync(Cancel);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (TaskCanceledException Exc)
            {
                throw new Exception("Task was canceled");
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
        public async Task<ActionResult> DeleteUser(Guid Id, CancellationToken Cancel)
        {
            try
            {
                await UnitOfWork.UserRepository.Delete(Id, Cancel);
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
        public async Task<ActionResult> UpdateUser(Guid Id, UserUpdateDTO User, CancellationToken Cancel)
        {
            try
            {
                if (!ModelsValidator.UserUpdateIsValid(User))
                    ModelState.AddModelError("user", "Contains errors in parametrs.");
                var UserToUpdate = await UnitOfWork.UserRepository.DbSet.FirstOrDefaultAsync(item => item.Id == Id);
                _Mapper.Map(User, UserToUpdate);
                UnitOfWork.UserRepository.Update(UserToUpdate);
                await UnitOfWork.SaveChangesAsync(Cancel);
                return Ok();
            }
            catch (DbUpdateException Exc)
            {
                throw new ArgumentException(Exc.Message);
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
    }
}
