using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Tools
{
    public interface ILoginService
    {
        bool IsConnected { get; }
        bool LoginConnection(string email, string password);

        bool TestConnection();

        void LogOut();

        int GetUserProfil();

        UserModel GetUser();
    }
}
