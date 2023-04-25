using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Deck
    {
        [Key]
        public string deck_id { get; set; }
        public string name { get; set; }
        [ForeignKey("Player")]
        public string player_id { get; set; } 
    }
}
