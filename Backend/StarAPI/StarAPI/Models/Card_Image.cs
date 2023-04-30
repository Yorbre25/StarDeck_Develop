using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class Card_Image
    {
        [Key]
        public int id { get; set; }

        public string image { get; set; }
    }
}
