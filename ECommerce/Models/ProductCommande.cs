using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ProductCommande
    {
        private int id;

        private int productId;

        private int commandeId;

        private int qty;

        public int Id { get => id; set => id = value; }
        public int ProductId { get => productId; set => productId = value; }
        public int CommandeId { get => commandeId; set => commandeId = value; }
        public int Qty { get => qty; set => qty = value; }

        [ForeignKey("CommandeId")]
        public virtual Commande Commande { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
