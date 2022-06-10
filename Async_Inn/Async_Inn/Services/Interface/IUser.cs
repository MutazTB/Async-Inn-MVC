using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Async_Inn.Services.Interface
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterDTO data, ModelStateDictionary modelState);

        public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal principal);
    }
}
