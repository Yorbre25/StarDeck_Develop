using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class Card_Type
    {
        [Key]
        public int id { get; set; }

        public string typeName { get; set; }
    }
}
