using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Deck
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Card")]
        public string cardId { get; set; }
        [ForeignKey("Player")]
        public string playerId { get; set; }
        [ForeignKey("Game")]
        public string gameId { get; set; }
    }
}
