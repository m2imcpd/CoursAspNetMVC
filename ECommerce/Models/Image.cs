using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Image
    {
        private int id;
        private string urlImage;

        public int Id { get => id; set => id = value; }
        public string UrlImage { get => urlImage; set => urlImage = value; }
    }
}
