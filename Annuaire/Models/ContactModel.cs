using Annuaire.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annuaire.Models
{
    public class ContactModel
    {

        private int id;
        private string nom;
        private string prenom;
        private string telephone;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }

        public static List<ContactModel> GetAllContacts()
        {
            DataDbContext data = new DataDbContext();
            return data.Contacts.ToList();
        }

        public static ContactModel GetContactById(int id)
        {
            DataDbContext data = new DataDbContext();
            return data.Contacts.Find(id);
        }
    }
}
