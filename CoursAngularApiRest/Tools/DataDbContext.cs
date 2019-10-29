using CoursAngularApiRest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursAngularApiRest.Tools
{
    public class DataDbContext : DbContext
    {

        public DataDbContext() : base()
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\coursAspNet;Integrated Security=True");
        }
    }
}
