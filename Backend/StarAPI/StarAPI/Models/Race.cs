using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class Race
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
