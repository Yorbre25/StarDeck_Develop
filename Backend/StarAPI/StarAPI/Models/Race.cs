using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class Race
    {
        [Key]
        public int race_id { get; set; }
        public string race_name { get; set; }
    }
}
