using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

// ORM Class of Race. Represents a Race in the database.
[Index(nameof(RaceName), IsUnique = true)]
public class Race
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string? RaceName { get; set; }

}
