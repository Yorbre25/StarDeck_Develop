using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Planets
    {
        [ForeignKey("Game")]
        public string gameId { get; set; }
        [ForeignKey("Planet")]
        public string planetId { get; set; }
    }
}
