using Annuaire.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annuaire.Tools
{
    public class DataDbContext : DbContext
    {
        public DataDbContext() : base()
        {

        }
        public DbSet<ContactModel> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\coursAspNet;Integrated Security=True");
        }
    }
}
