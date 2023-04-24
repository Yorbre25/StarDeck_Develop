using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

// ORM Class of Country. Represents a Country in the database.
public partial class Country
{   
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? CountryName { get; set; }

}
