using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Player_Card
    {
        [ForeignKey("Player")]
        public string playerId { get; set; }
        [ForeignKey("Card")]
        public string cardId { get; set; }
    }
}
