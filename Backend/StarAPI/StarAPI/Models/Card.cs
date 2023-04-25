using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Card
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public int energy { get; set; }
        public int cost { get; set; }
        [ForeignKey("Card_Type")]
        public int  card_type_id { get; set; }
        [ForeignKey("Race")]
        public int card_race_id { get; set; }
        public bool activated_card { get; set; }
        public string description { get; set; }
    }
}
