using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class GameTable
    {
        [Key]
        public string gameId { get; set; }
        public string planetId { get; set; }
        public string playerId { get; set; }
        public string cardId { get; set; }
    }
}
