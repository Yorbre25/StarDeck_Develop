using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Player
    {
        [ForeignKey("Game")] 
        public string gameId { get; set; }
        [ForeignKey("Player")]
        public string playerId { get; set; }
        [ForeignKey("Deck")]
        public string deckId { get; set; }
        public int cardPoints { get; set; }
        public int maxCardPoints { get; set; }
    }
}
