using CorrectionApiRestDeezer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorrectionApiRestDeezer.Tools
{
    public class DataDbContext : DbContext
    {

        public DataDbContext() : base()
        {

        }

        public DbSet<UserApi> Users { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public DbSet<PlayList> PlayLists { get; set; }

        public DbSet<TrackPlayList> TracksPlayLists { get; set; }

        public DbSet<Like> Likes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\correctionDeezer;Integrated Security=True");
        }
    }
}
