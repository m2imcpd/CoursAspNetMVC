using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Categorie
    {
        private int id;
        private string titre;


        public int Id { get => id; set => id = value; }
        public string Titre { get => titre; set => titre = value; }
        public int ImageId { get; set; }

        [ForeignKey("ImageId")]
        public virtual Image ImageCategorie { get; set; }

        public virtual ICollection<ProductCategorie> Products { get; set; }

        public Categorie()
        {
            Products = new List<ProductCategorie>();
        }
    }
}
