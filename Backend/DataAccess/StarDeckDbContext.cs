using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

// ORM Class of StarDeckDbContext. Represents the database in the application.
public class StarDeckDbContext : DbContext
{

    public StarDeckDbContext(DbContextOptions<StarDeckDbContext> options) : base(options) {}

    public  DbSet<Race>? Races { get; set; }
    public  DbSet<CardType>? CardTypes { get; set; }
    public  DbSet<Card>? Cards { get; set; }

    public  DbSet<Country>? Countries { get; set; }

    public DbSet<Player>? Players { get; set; }

}
