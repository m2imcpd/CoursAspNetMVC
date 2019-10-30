using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestApiJWT.Models;

namespace TestApiJWT.Tools
{
    public class UserService : IUserService
    {
        private List<UserModel> liste = new List<UserModel>() { new UserModel
        {
            Id = 1,
            Nom = "ABADI",
            Prenom = "IHAB",
            Email = "ihab@utopios.net",
            Password = "123456"
        }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserModel Authenticate(string email, string password)
        {
            UserModel user = liste.SingleOrDefault(x => x.Email == email && x.Password == password);
            if (user == null)
                return null;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secrect);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Nom),
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(descriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }
    }

    public interface IUserService
    {
        UserModel Authenticate(string email, string password);
    }
}
