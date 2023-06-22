using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game
    {
        [Key]
        public string id { get; set; }
        public int maxTurns { get; set; }
        public int endTurnCounter { get; set; }
        public int endGameCounter { get; set; }
        public int xpGain { get; set; } 
        public int turn { get; set; }
        public DateTime timeStarted { get; set; }
        [ForeignKey("Game_Player")]
        public string player1Id { get; set; }
        [ForeignKey("Game_Player")]
        public string player2Id { get; set; }

    }
}
