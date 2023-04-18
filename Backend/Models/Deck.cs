using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Deck
{
    public string DeckId { get; set; } = null!;

    public string CardId { get; set; } = null!;

    public virtual Card Card { get; set; } = null!;
}
