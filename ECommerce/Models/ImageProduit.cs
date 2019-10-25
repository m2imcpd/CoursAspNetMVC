using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ImageProduit
    {
        private int id;

        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public int Id { get => id; set => id = value; }
        [ForeignKey("ImageId")]
        public virtual Image Image {get;set;}
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
