using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class StarDeckDbContext : DbContext
{
    public StarDeckDbContext()
    {
    }

    public StarDeckDbContext(DbContextOptions<StarDeckDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; } = null!;

    public virtual DbSet<CardType> CardTypes { get; set; } = null!;

    public virtual DbSet<Deck> Decks { get; set; } = null!;

    public virtual DbSet<Race> Races { get; set; } = null!;

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer(" Server=tcp:dot-coder-server.database.windows.net,1433;Initial Catalog=StarDeckDB;Persist Security Info=False;User ID=yorbre25;Password=Yraulbr25;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Card__3213E83FBFEAC45F");

            entity.ToTable("Card");

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("id");

            entity.Property(e => e.ActivatedCard)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("activated_card");

            entity.Property(e => e.CardRaceId).HasColumnName("card_race_id");

            entity.Property(e => e.CardTypeId).HasColumnName("card_type_id");

            entity.Property(e => e.Cost).HasColumnName("cost");

            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("description");

            entity.Property(e => e.Energy).HasColumnName("energy");

            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasDefaultValueSql("('https://i.imgur.com/1ZQZ1Zm.png')")
                .HasColumnName("image");

            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.CardRace).WithMany(p => p.Cards)
                .HasForeignKey(d => d.CardRaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Card_Race");

            entity.HasOne(d => d.CardType).WithMany(p => p.Cards)
                .HasForeignKey(d => d.CardTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Card_Type");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Card_Typ__2C000598A093FEC9");

            entity.ToTable("Card_Type");

            entity.HasIndex(e => e.TypeName, "UQ__Card_Typ__543C4FD93A1AF23C").IsUnique();

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Deck>(entity =>
        {
            entity.HasKey(e => new { e.DeckId, e.CardId }).HasName("PK__Deck__E80518CDDC7F9947");

            entity.ToTable("Deck");

            entity.Property(e => e.DeckId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("deck_id");

            entity.Property(e => e.CardId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("card_id");

            entity.HasOne(d => d.Card).WithMany(p => p.Decks)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Deck_Card");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.RaceId).HasName("PK__Race__1C8FE2F95F6EF203");

            entity.ToTable("Race");

            entity.HasIndex(e => e.RaceName, "UQ__Race__4A929FC9EF311AF7").IsUnique();

            entity.Property(e => e.RaceId).HasColumnName("race_id");

            entity.Property(e => e.RaceName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("race_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
