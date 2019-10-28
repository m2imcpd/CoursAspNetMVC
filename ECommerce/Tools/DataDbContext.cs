using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Tools
{
    public class DataDbContext : DbContext
    {
        public DataDbContext() : base()
        {

        }

        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageProduit> ImagesProduct { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<ProductCategorie> ProductCategories { get; set; }

        public DbSet<Commande> Commandes { get; set; }
        public DbSet<ProductCommande> ProductsCommandes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\coursAspNet;Integrated Security=True");
        }
    }
}
