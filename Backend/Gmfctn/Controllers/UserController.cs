using Data_.Dtos;
using Data_.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gmfctn.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;

        public UserController(IUserService _UserService)
        {
            UserService = _UserService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsers(CancellationToken Cancel)
        {
           
            try
            {
                var Users = await UserService.GetAllUsers(Cancel);
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
        [HttpGet("{Id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserById(Guid Id, CancellationToken Cancel)
        {
            try
            {
                var User = await UserService.GetUserById(Id, Cancel);
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
                if (await UserService.CreateUser(NewUser, Cancel))
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
        public async Task<ActionResult> DeleteUser(Guid Id, CancellationToken Cancel)
        {
            try
            {
                if (await UserService.DeleteUser(Id, Cancel))
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
        public async Task<ActionResult> UpdateUser(Guid Id, UserUpdateDTO User, CancellationToken Cancel)
        {
            try
            {
                if (await UserService.UpdateUser(Id, User, Cancel))
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
                throw new ArgumentException(Exc.Message);
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
    }
}
