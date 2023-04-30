using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class PlanetType
    {
        [Key]
        public int id { get; set; }

        public string typeName { get; set; }
    }
}
