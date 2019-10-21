using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ProductCategorie
    {
        private int id;
        

        public int Id { get => id; set => id = value; }

        public int ProductId { get; set; }

        public int CategorieId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("CategorieId")]
        public virtual Categorie Categorie { get; set; }
    }
}
