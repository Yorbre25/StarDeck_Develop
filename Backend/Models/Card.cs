using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public partial class Card
{   

    public string Id { get; set; } 

    [Required]
    [MaxLength(30)]
    public string Name { get; set; } 

    [Required]
    // Max value 100. Min value -100.
    public int Energy { get; set; }

    [Required]
    // Max value 100. Min value 0.
    public int Cost { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Image { get; set; }

    [DefaultValue(1)]
    public bool ActivatedCard { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public virtual Race CardRace { get; set; } 

    [Required]
    public virtual CardType CardType { get; set; } 

}
