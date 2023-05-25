using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Planet
    {
        [ForeignKey("Game")]
        public string gameId { get; set; }
        [ForeignKey("Planet")]
        public string planetId { get; set; }
        public bool show { get; set; }
    }
}
