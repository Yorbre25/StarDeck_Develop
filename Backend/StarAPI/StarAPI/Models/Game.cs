using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game
    {
        [Key]
        public string id { get; set; }
        public string player1Id { get; set; }
        public string player2Id { get; set; }
    }
}
