using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Deck
    {
        [ForeignKey("Deck")]
        public string deckId { get; set; }
        [ForeignKey("Player")]
        public string playerId { get; set; }
    }
}
