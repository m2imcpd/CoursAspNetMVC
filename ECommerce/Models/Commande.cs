using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Commande
    {
        private int id;
        private decimal total;
        private int clientId;
        

        public int Id { get => id; set => id = value; }
        public decimal Total { get => total; set => total = value; }

        [ForeignKey("ClientId")]
        public virtual UserModel Client { get; set; }
        public int ClientId { get => clientId; set => clientId = value; }

        public ICollection<ProductCommande> Products { get; set; }

        public Commande()
        {
            Products = new List<ProductCommande>();
        }
    }
}
