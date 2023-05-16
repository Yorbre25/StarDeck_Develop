using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Hand
    {
        [Key]
        public string id { get; set; }
        [Key]
        [ForeignKey("Game_Player")]
        public string playerId { get; set; }
        
    }
}