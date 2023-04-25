using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class Card_Type
    {
        [Key]
        public int type_id { get; set; }
        public string type_name { get; set; }
    }
}
