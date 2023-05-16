using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Match_Player
    {
        [Key]
        [ForeignKey("Player")]
        public string id { get; set; }
        public DateTime waiting_since { get; set; }
    }
}
