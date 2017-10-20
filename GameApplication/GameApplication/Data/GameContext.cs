using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace GameApplication.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
