using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Planet
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        [ForeignKey("PlanetType")]
        public int typeId { get; set; }
        public bool activatedPlanet { get; set; }
        public string description { get; set; }
        public int imageId { get; set; }
        
        
    }
}
