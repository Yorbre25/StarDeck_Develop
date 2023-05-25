using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Player
    {
        // [Key]
        // public string id {get; set;}
        [ForeignKey("Player")]
        public string playerId { get; set; }
        [ForeignKey("Deck")]
        public string deckId { get; set; }
        [ForeignKey("Game")] 
        public string gameId { get; set; }
        // public string handId { get; set; }
    }
}
