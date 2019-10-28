using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursAPI.Model
{
    public class Personne
    {
        private string nom;
        private string prenom;

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
    }
}
