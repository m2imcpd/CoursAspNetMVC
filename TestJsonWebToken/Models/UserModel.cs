using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJsonWebToken.Models
{
    public class UserModel
    {
        private int id;
        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string token;

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Token { get => token; set => token = value; }
        public string Password { get => password; set => password = value; }
    }
}
