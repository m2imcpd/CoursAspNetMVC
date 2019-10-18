using CoursAspNet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursAspNet.Tools
{
    public class DataDbContext : DbContext
    {

        public DataDbContext() : base()
        {

        }

        public DbSet<PersonneModel> PersonnesASP { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\CoursMCPD;Integrated Security=True");
        }
    }
}
