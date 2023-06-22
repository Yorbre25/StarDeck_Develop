using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class CardType
    {
        [Key]
        public int id { get; set; }

        public string typeName { get; set; }
    }
}
