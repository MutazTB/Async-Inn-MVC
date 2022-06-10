using Async_Inn.Models.DTOs;
using Async_Inn.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Controllers
{
    public class UsersController : Controller
    {
        private IUser _userService;

        public UsersController(IUser service)
        {
            _userService = service;
        }
        [Authorize(Roles = "District Manager")]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO data)
        {
            try
            {
                await _userService.Register(data, this.ModelState);
                if (ModelState.IsValid)
                {
                    return Ok("Registered done");

                }
                return BadRequest(new ValidationProblemDetails(ModelState));
                //return Ok("A User hase beed registed successfully");

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Signin")]
        public async Task<ActionResult<UserDTO>> Signin(LoginDTO data)
        {
            var user = await _userService.Authenticate(data.Username, data.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            return user;
        }

        [HttpGet("me")]
        [Authorize(Policy = "create")]
        public async Task<ActionResult<UserDTO>> Me()
        {
            // Following the [Authorize] phase, this.User will be ... you.
            // Put a breakpoint here and inspect to see what's passed to our getUser method
            return await _userService.GetUser(this.User);
        }
    }
}
