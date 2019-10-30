using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJsonWebToken.Models;
using TestJsonWebToken.Tools;

namespace TestJsonWebToken.Controllers
{
    [Route("[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ILoginService serviceLogin;
        public UsersController(ILoginService _serviceLogin)
        {
            serviceLogin = _serviceLogin;
        }
        [HttpPost]
        public ActionResult Login([FromBody]UserModel u)
        {
            UserModel user = serviceLogin.LogIn(u.Email, u.Password);
            if(user == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(new { message = user.Token });
            }
            
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetInfo()
        {
            return Ok(new { message = "Access Ok" });
        }

        [HttpGet]
        [Route("/testConnection")]
        [Authorize]
        public ActionResult TestConnection()
        {
            return Ok(new { message = "Access OK" });
        }
    }
}
