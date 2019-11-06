using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CorrectionApiRestDeezer.Models;
using CorrectionApiRestDeezer.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorrectionApiRestDeezer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DataDbContext data;

        private ILoginService loginService;
        
        public UserController(DataDbContext _data,  ILoginService _loginService)
        {
            data = _data;
            loginService = _loginService;
        }
        [Route("/user/add")]
        [HttpPost]
        public ActionResult AddUser()
        {
            IFormFile avatar = Request.Form.Files[0];
            UserApi user = new UserApi();
            if (avatar.Length > 0)
            {
                string pathFolder = Path.Combine("wwwroot", "avatar", Path.GetFileName(avatar.FileName));
                FileStream s = new FileStream(pathFolder, FileMode.Create);
                user.AvatarUrl = "http://" + Request.Host + "/avatar/" + Path.GetFileName(avatar.FileName);
                avatar.CopyTo(s);
                s.Close();   
            }
            user.FirstName = Request.Form["firstName"].ToString();
            user.LastName = Request.Form["lastName"].ToString();
            user.Password = Request.Form["password"].ToString();
            user.Email = Request.Form["email"].ToString();
            user.Role = 1;
            data.Users.Add(user);
            if (data.SaveChanges() > 0)
            {
                return Ok(new { error = false, userId = user.Id });
            }
            else
            {
                return StatusCode(500);
            }
        }

        [Route("/login")]
        [HttpPost]
        public ActionResult Login([FromBody] dynamic user)
        {
            string token = loginService.Login((string)user.email, (string)user.password);
            if(token == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new { error = false, token = token });
            }
            
        }

        [Route("/user/update")]
        [HttpPut]
        [Authorize]
        public ActionResult UpdateUser()
        {
            int userId;
            Int32.TryParse((User.Claims.ToList()[1]).Value, out userId);
            IFormFile avatar = Request.Form.Files[0];
            UserApi user = data.Users.Find(userId);
            if (avatar.Length > 0)
            {
                string pathFolder = Path.Combine("wwwroot", "avatar", Path.GetFileName(avatar.FileName));
                FileStream s = new FileStream(pathFolder, FileMode.Create);
                user.AvatarUrl = "http://" + Request.Host + "/avatar/" + Path.GetFileName(avatar.FileName);
                avatar.CopyTo(s);
                s.Close();

                
            }
            user.FirstName = Request.Form["firstName"].ToString() ?? user.FirstName;
            //user.FirstName = Request.Form["firstName"].ToString() == null ? Request.Form["firstName"].ToString() : user.FirstName;
            user.LastName = Request.Form["lastName"].ToString() ?? user.LastName;
            user.Password = Request.Form["password"].ToString() ?? user.Password;
            data.Users.Add(user);
            if (data.SaveChanges() > 0)
            {
                return Ok(new { error = false, userId = user.Id });
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}