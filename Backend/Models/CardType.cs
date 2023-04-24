using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

// ORM Class of CardType. Represents a CardType in the database.
[Index(nameof(TypeName), IsUnique = true)]
public class CardType
{   
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? TypeName { get; set; }

}
