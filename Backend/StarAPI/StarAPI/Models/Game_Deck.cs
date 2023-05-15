using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarAPI.Models
{
    public class Game_Deck
    {
        [Key]
        public string deckId { get; set; }
        [Key]
        public string cardId { get; set; }
    }
}
