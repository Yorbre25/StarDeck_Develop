using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Card
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Energy { get; set; }

    public int Cost { get; set; }

    public string? Image { get; set; }

    public int CardTypeId { get; set; }

    public int CardRaceId { get; set; }

    public bool? ActivatedCard { get; set; }

    public string? Description { get; set; }

    public virtual Race CardRace { get; set; } = null!;

    public virtual CardType CardType { get; set; } = null!;

    public virtual ICollection<Deck> Decks { get; set; } = new List<Deck>();
}
