using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class TurnPlayer
    {
        [Key]
        [ForeignKey("Player")]
        public string playerId { get; set; }

        [ForeignKey("Game")]
        public string gameId { get; set; }
        public bool inTurn { get; set; }

    }
}
