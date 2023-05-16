using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class GameTable
    {
        [Key]
        public string id { get; set; }
        public string planet1Id { get; set; }
        public string planet2Id { get; set; }
        public string planet3Id { get; set; }
    }
}
