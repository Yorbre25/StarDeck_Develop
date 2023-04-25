using StarAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace StarAPI.Context
{
    public class StarDeckContext : DbContext
    {
        public StarDeckContext(DbContextOptions<StarDeckContext> options) : base(options)
            
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(c => new {c.id });
            modelBuilder.Entity<Country>().HasKey(c => new { c.id });
        }

        public DbSet<Player> Player { get; set; }
        public DbSet<Country> Country { get; set; }
    }

   
}
