using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Deck_Card
    {
        [ForeignKey("Deck")]
        public string deck_id { get; set; }
        [ForeignKey("Card")]
        public string card_id { get; set; }
    }
}
