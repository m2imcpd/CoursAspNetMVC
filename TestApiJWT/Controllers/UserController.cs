using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiJWT.Models;
using TestApiJWT.Tools;

namespace TestApiJWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody]UserModel userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var authenticateInfo =  await HttpContext.Authentication.GetAuthenticateInfoAsync("Bearer");
            return Ok(new { message = "Access ok" });
        }

    }
}
