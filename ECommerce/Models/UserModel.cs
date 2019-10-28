using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class UserModel
    {
        private int id;
        private string nom;
        private string prenom;
        private string email;
        private string password;
        private int typeProfil;
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int TypeProfil { get => typeProfil; set => typeProfil = value; }

        public ICollection<Commande> Commandes { get; set; }

        public UserModel()
        {
            Commandes = new List<Commande>();
        }
    }
}
