using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Player_Card
    {
        [ForeignKey("Player")]
        public string player_id { get; set; }
        [ForeignKey("Card")]
        public string card_id { get; set; }
    }
}
