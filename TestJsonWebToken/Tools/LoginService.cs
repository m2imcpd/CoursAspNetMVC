using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestJsonWebToken.Models;

namespace TestJsonWebToken.Tools
{
    public class LoginService : ILoginService
    {
        private string secret = "je suis une chaine secret";
        private List<UserModel> listeUsers = new List<UserModel>()
        {
            new UserModel {Id = 1, FirstName = "Ihab", LastName="ABADI", Email="ihab@utopios.net", Password="123456"}
        };
        public UserModel LogIn(string email, string password)
        {
            UserModel user = listeUsers.SingleOrDefault(x => x.Email == email && x.Password == password);
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
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Country, "France"),
            }) ;
            //date d'expiration du JWT
            descriptor.Expires = DateTime.Now.AddHours(1);
            //Algo pour signier le JWT
            descriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            //Création du token
            SecurityToken token = tokenHandler.CreateToken(descriptor);
            //Convertir le token en chaine de caractère et le stocker dans le Token d'User
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }
    }

    public interface ILoginService
    {
        UserModel LogIn(string email, string password);
    }
}
