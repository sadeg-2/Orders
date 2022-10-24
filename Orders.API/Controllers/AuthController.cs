
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Data.Models;
using Orders.Infrastructure.Services.Auth;
using System;


namespace Orders.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService ;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthService authService)
        {

            _authService = authService;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] loginDto dto)
        {
            return Ok(GetAPIResponse(_authService.login(dto)));
        }


    }
}
