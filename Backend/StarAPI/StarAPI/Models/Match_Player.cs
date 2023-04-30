using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Match_Player
    {
        [ForeignKey("Player")]
        public string id { get; set; }
        public DateTime waiting_since { get; set; }
    }
}
