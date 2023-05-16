using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Player
    {
        // [Key]
        // public string id {get; set;}
        public string playerId { get; set; }
        public string deckId { get; set; } 
        // public string handId { get; set; }
    }
}
