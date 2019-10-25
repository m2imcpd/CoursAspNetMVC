using ECommerce.Models;
using Microsoft.AspNetCore.Http;
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
    }
}
