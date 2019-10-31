using CoursAPI.Model;
using CoursAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILoginService _loginService;

        public UserController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [Route("/login")]
        [HttpPost]
        public ActionResult Login([FromBody] UserModel user)
        {
            string token = _loginService.LogIn(user.Email, user.Password);
            if(token == null)
            {
                return Ok(new { error = true, message = "aucun utilisateur" });
            }
            else
            {
                return Ok(new { error = false, token = token });
            }
        }

        [Authorize]
        [Route("/testLogin")]
        [HttpPost]
        public ActionResult TestLogin()
        {
            return Ok(new { error = false });
        }
    }
}
