using StarAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace StarAPI.Context
{
    // This class is used to create the database context for the StarDeck database.
    public class StarDeckContext : DbContext
    {
        public StarDeckContext()
        {
        }
        public StarDeckContext(DbContextOptions<StarDeckContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        // @description
        // This method is used to create the database tables for the StarDeck database.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasKey(c => new {c.id });
            modelBuilder.Entity<Country>().HasKey(c => new { c.id });
            modelBuilder.Entity<Card>().HasKey(c => new { c.id });
            modelBuilder.Entity<Race>().HasKey(c => new { c.id });
            modelBuilder.Entity<CardType>().HasKey(c => new { c.id });
            modelBuilder.Entity<Deck>().HasKey(c => new { c.id });
            modelBuilder.Entity<Deck_Card>().HasKey(c => new { c.deckId, c.cardId });
            modelBuilder.Entity<Player_Card>().HasKey(c => new { c.playerId, c.cardId });
            modelBuilder.Entity<PlanetType>().HasKey(c => new { c.id });
            modelBuilder.Entity<Planet>().HasKey(c => new { c.id });
            modelBuilder.Entity<Match_Player>().HasKey(c => new { c.id });
            modelBuilder.Entity<Game>().HasKey(c => new { c.id });
            modelBuilder.Entity<GameTable>().HasKey(c => new { c.id });
            modelBuilder.Entity<Game_Planets>().HasKey(c => new { c.gameId, c.planetId });
            modelBuilder.Entity<Game_Player>().HasKey(c => new { c.playerId });
            modelBuilder.Entity<Game_Deck>().HasKey(c => new { c.playerId, c.deckId });
            modelBuilder.Entity<Game_Deck_Card>().HasKey(c => new {c.deckId, c.cardId });
            modelBuilder.Entity<SetupParam>().HasKey(c => new { c.id });
            modelBuilder.Entity<Hand_Card>().HasKey(c => new { c.handId, c.cardId });
            modelBuilder.Entity<Hand>().HasKey(c => new { c.id });

        }

        public DbSet<Player> Player { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Race> Race { get; set; }
        public DbSet<CardType> CardType { get; set; }
        public DbSet<Deck> Deck { get; set; }
        public DbSet<Deck_Card> Deck_Card { get; set; }
        public DbSet<Player_Card> Player_Card { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<PlanetType> PlanetType { get; set; }
        public DbSet<Planet> Planet { get; set; }
        public DbSet<Match_Player> Match_Player { get; set; }
        public DbSet<SetupParam> SetupParam { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameTable> GameTable { get; set; }
        public DbSet<Game_Player> Game_Player { get; set; }
        public DbSet<Game_Deck> Game_Deck { get; set; }
        public DbSet<Game_Deck_Card> Game_Deck_Card { get; set; }
        public DbSet<Hand_Card> Hand_Card { get; set; }
        public DbSet<Hand> Hand { get; set; }
    }

   
}
