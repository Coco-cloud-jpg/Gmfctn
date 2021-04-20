using Data_.Dtos;
using Data_.Entities;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;
        public UserController(IUserService _UserService)
        {
            UserService = _UserService;
        }

        [HttpGet("get_all_users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsers(CancellationToken Cancel)
        {
            try
            {
                var Users = await UserService.GetAllUsers(Cancel);
                if (Users == null)
                    return NoContent();
                else
                    return Ok(Users);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("get_user/{Id}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<UserReadDTO>> GetUserById(Guid Id, CancellationToken Cancel)
        {
            try
            {
                var User = await UserService.GetUserById(Id, Cancel);
                if (User == null)
                {
                    return NoContent();
                }
                return Ok(User);
            }
            catch 
            {
                return BadRequest();
            }
        }
        [HttpGet("get_all_users_info")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<UserReadShortDTO>> GetAllUsersInfo(CancellationToken Cancel)
        {
            try
            {
                var User = await UserService.GetAllUsersInfo(Cancel);
                if (User == null)
                {
                    return NoContent();
                }
                return Ok(User);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("get_user_info/{Id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<UserReadShortDTO>> GetUserInfoById(Guid Id, CancellationToken Cancel)
        {
            try
            {
                var User = await UserService.GetUserInfoById(Id, Cancel);
                if (User != null)
                {
                    return NotFound();
                }
                return Ok(User);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateUser(UserCreateDTO NewUser, CancellationToken Cancel)
        {
            try
            {
                await UserService.CreateUser(NewUser, Cancel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(Guid Id, CancellationToken Cancel)
        {
            try
            {
                await UserService.DeleteUser(Id, Cancel);
                return NoContent();
            }
            catch (ArgumentException ArgExp)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateUser(Guid Id, UserUpdateDTO User, CancellationToken Cancel)
        {
            try
            {
                await UserService.UpdateUser(Id, User, Cancel);
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
