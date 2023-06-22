using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Deck
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        [ForeignKey("Player")]
        public string playerId { get; set; } 
    }
}
