using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Deck_Card
    {
        [ForeignKey("Deck")]
        public string deckId { get; set; }
        [ForeignKey("Card")]
        public string cardId { get; set; }
    }
}
