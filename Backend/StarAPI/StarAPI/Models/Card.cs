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
        [ForeignKey("CardType")]
        public int  typeId { get; set; }
        [ForeignKey("Race")]
        public int raceId { get; set; }
        public bool activatedCard { get; set; }
        public string description { get; set; }
        [ForeignKey("Card_Image")]
        public int imageId { get; set; }
    }
}
