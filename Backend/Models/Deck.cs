using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Deck
{
    public string Id { get; set; }

    public List<Card> Cards { get; set; } = new List<Card>();
}
