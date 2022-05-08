using Async_Inn.Models.DTOs;
using Async_Inn.Services.Interface;
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

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO data)
        {
            var user = await _userService.Register(data, this.ModelState);

            if (ModelState.IsValid)
            {
                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
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
    }
}
