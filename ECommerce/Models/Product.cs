using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Product
    {
        private int id;
        private string title;
        private string description;
        private decimal price;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public decimal Price { get => price; set => price = value; }

        [JsonIgnore]
        public virtual ICollection<ProductCategorie> Categories { get; set; }
        public virtual ICollection<ImageProduit> Images { get; set; }

        public virtual ICollection<ProductCommande> Commandes { get; set; }
        public Product()
        {
            Categories = new List<ProductCategorie>();
            Images = new List<ImageProduit>();
            Commandes = new List<ProductCommande>();
        }
    }
}
