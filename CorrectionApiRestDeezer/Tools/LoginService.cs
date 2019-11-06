using CorrectionApiRestDeezer.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CorrectionApiRestDeezer.Tools
{
    public class LoginService : ILoginService
    {
        private DataDbContext data;

        public LoginService(DataDbContext _data)
        {
            data = _data;
        }
        public string Login(string email, string password)
        {
            UserApi user = data.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
            if (user == null)
                return null;
            //Objet pour creer un json web Token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //Convertir la chaine de cryptage en bytes
            byte[] key = Encoding.ASCII.GetBytes("je suis une chaine secret");
            //Description du contenu du JWT
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor();
            descriptor.Subject = new ClaimsIdentity(new Claim[]
            {
                //Contenu du body du JWT
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            });
            //date d'expiration du JWT
            descriptor.Expires = DateTime.Now.AddDays(1);
            //Algo pour signier le JWT
            descriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            //Création du token
            SecurityToken token = tokenHandler.CreateToken(descriptor);
            //Convertir le token en chaine de caractère et le stocker dans le Token d'User
            user.Token = tokenHandler.WriteToken(token);
            data.SaveChanges();
            return user.Token;
        }
    }
}
