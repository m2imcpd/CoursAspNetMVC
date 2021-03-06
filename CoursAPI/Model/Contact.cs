﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursAPI.Model
{
    public class Contact
    {
        private int id;
        private string nom;
        private string prenom;
        private string email;
        private string urlImage;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Email { get => email; set => email = value; }
        public string UrlImage { get => urlImage; set => urlImage = value; }
    }
}
