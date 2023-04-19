﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public partial class CardType
{   
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]

    public string TypeName { get; set; }

}
