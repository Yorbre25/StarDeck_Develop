using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class Image
    {
        [Key]
        public int id { get; set; }

        public string imageString { get; set; }
    }
}
