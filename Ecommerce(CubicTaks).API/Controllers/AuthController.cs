using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Application.Service;
using Ecommerce_CubicTaks_.Application.Service.Jwt;
using Ecommerce_CubicTaks_.Dto.Customer;
using Ecommerce_CubicTaks_.Dto.User;
using Ecommerce_CubicTaks_.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;

namespace Ecommerce_CubicTaks_.API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        public AuthController(IJwtService jwtService, IUserService userService = null)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.LoginAsync(model);
            if (!result.IsSuccess)
                return Unauthorized();

            return Ok(new
            {
                token = result.Entity
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterUserAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Entity); // will return UserDto
        }


    }
}
