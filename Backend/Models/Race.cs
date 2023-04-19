using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

// [PrimaryKey(nameof(Id), nameof(RaceName))]
public partial class Race
{
    public int Id { get; set; }

    [MaxLength(50)]
    // RaceName needs to be unique for each Id.
    public string RaceName { get; set; }

}
