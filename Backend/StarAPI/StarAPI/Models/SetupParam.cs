using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class SetupParam
    {
        [Key]
        public int id {get; set;}
        public int totalTurns { get; set; }
        public int timePerTurn { get; set; }
        public DateTime date { get; set; }
    }
}
