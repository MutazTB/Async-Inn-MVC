using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Services.Implementation;
using Async_Inn.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Async_Inn.Services
{
    public class IdentityUserService : IUser 
    {
        private UserManager<ApplicationUser> _userManager;
        private JwtTokenService tokenService;

        public IdentityUserService(UserManager<ApplicationUser> manager, JwtTokenService jwtTokenService)
        {
            _userManager = manager;
            tokenService = jwtTokenService;
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                return new UserDTO
                {
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromDays(2)),
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }
            return null;
        }

        public async Task<UserDTO> Register(RegisterDTO data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                // here goes the roles specifications ... 
                IList<string> Roles = new List<string>();
                Roles.Add("administrator");
                await _userManager.AddToRolesAsync(user, Roles);

                return new UserDTO
                {
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, TimeSpan.FromDays(2)),
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return new UserDTO
            {
                Username = user.UserName
            };
        }
    }
}

