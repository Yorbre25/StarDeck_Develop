using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class CardType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
