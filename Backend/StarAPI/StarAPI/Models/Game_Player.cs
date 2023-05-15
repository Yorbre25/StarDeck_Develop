using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Player
    {
        [Key]
        public string playerId { get; set; }
        public string gameId { get; set; }
        public string gameDeckId { get; set; } 
    }
}
