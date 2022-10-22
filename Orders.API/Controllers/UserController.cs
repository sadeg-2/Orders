using Microsoft.AspNetCore.Mvc;
using Orders.Core.Dtos;
using Orders.Infrastructure.Services.Users;

namespace Orders.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpGet]
        public IActionResult GetAll(string searchKey)
        {
            var users = _userService.GetAll(searchKey);
            return Ok(GetAPIResponse(users));
        }
        [HttpGet]
        public IActionResult Get(string id)
        {
            var user = _userService.Get(id);
            return Ok(GetAPIResponse(user));
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDto dto)
        {
            var result = _userService.Create(dto);
            return Ok(GetAPIResponse(result));
        }
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var result = _userService.Delete(id);
            return Ok(GetAPIResponse(result));
        }
        [HttpPost]
        public IActionResult Update(UpdateUserDto dto)
        {
            var save = _userService.Update(dto);
            return Ok(GetAPIResponse(save));
        }
    }

}
