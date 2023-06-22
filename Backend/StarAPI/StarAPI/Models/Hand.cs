using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Hand
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("Card")]
        public string cardId { get; set; }
        [ForeignKey("Game_Player")]
        public string playerId { get; set; }
        [ForeignKey("Game")]
        public string gameId { get; set; }
        
    }
}