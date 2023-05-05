using System.ComponentModel.DataAnnotations;

namespace StarAPI.Models
{
    public class SetupValues
    {
        [Key]
        public int totalTurns { get; set; }
        public string timePerTurn { get; set; }
    }
}
