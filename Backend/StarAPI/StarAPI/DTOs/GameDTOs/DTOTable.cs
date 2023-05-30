using StarAPI.DTO.Discovery;

namespace StarAPI.DTO.Game
{

    public class InputTableLayout
    {
        public string gameId { get; set; }
        public string playerId { get; set; }
        //planetId, cardId
        public Dictionary<string, string> layout { get; set; }

    }

    public class OutputTableLayout
    {
        public Dictionary<string, OutputCard> playerCards { get; set; }
        public Dictionary<string, OutputCard> rivalCards { get; set; }

    }

}