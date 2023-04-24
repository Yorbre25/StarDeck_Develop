using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

// ORM Class of Player. Represents a Player in the database.
public class Player
{   

    [Required]
    [MaxLength(14)]
    public string? Id { get; set; } 

    [Required]
    [MaxLength(50)]
    public string? FirstName { get; set; } 

    // [Required]
    // [MaxLength(100)]
    // public string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Username { get; set; }

    [Required]
    [DefaultValue(1)]
    public bool ActivatedAccount { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Password { get; set; }

    // [Required]
    // [DefaultValue(0)]
    // public string Level { get; set; } 

    [Required]
    [DefaultValue("https://i.imgur.com/1Z1Z1Z1.png")]
    public string? Avatar { get; set; } 

    // [Required]
    // [DefaultValue(0)]
    // public int XPPoint { get; set; }

    // [Required]
    // [DefaultValue(20)]
    // public int money { get; set; }

    [Required]
    [DefaultValue(0)]
    public int Ranking { get; set; }

    [Required]
    public Country? Country { get; set; }

    [Required]
    public List<Card>? Cards { get; set; }

}
