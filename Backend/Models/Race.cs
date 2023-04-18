using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Race
{
    public int RaceId { get; set; }

    public string? RaceName { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
