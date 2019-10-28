using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Tools
{
    public class LoginService : ILoginService
    {

        private bool isConnected;
        public bool IsConnected => isConnected;

        private DataDbContext data;
        private IHttpContextAccessor accessor;
        public LoginService(DataDbContext _data, IHttpContextAccessor _accessor)
        {
            data = _data;
            accessor = _accessor;
            isConnected = TestConnection();
        }
        public bool LoginConnection(string email, string password)
        {
            UserModel u = data.Users.FirstOrDefault((x) => x.Email == email && x.Password == password);
            if(isConnected = u != null)
            {
                //Acceder au cookies en utilisant un service de type IHttpContextAccessor
                accessor.HttpContext.Response.Cookies.Append("userEmail", email, new CookieOptions { Expires = DateTime.Now.AddDays(1) });
                accessor.HttpContext.Response.Cookies.Append("userPassword", password, new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            }
            return isConnected;
        }

        public bool TestConnection()
        {
            string email = accessor.HttpContext.Request.Cookies["userEmail"];
            string password = accessor.HttpContext.Request.Cookies["userPassword"];
            UserModel u = data.Users.FirstOrDefault((x) => x.Email == email && x.Password == password);
            return u != null;
        }
        public void LogOut()
        {
            accessor.HttpContext.Response.Cookies.Append("userEmail", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            accessor.HttpContext.Response.Cookies.Append("userPassword", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            isConnected = false;
        }

        public int GetUserProfil()
        {
            string email = accessor.HttpContext.Request.Cookies["userEmail"];
            string password = accessor.HttpContext.Request.Cookies["userPassword"];
            UserModel u = data.Users.FirstOrDefault((x) => x.Email == email && x.Password == password);
            if(u != null)
            {
                return u.TypeProfil;
            }
            else
            {
                return 0;
            }
        }

        public UserModel GetUser()
        {
            string email = accessor.HttpContext.Request.Cookies["userEmail"];
            string password = accessor.HttpContext.Request.Cookies["userPassword"];
            UserModel u = data.Users.Include("Commandes").FirstOrDefault((x) => x.Email == email && x.Password == password);
            return u;
        }
    }
}
