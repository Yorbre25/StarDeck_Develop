
namespace StarAPI.DTO.Discovery
{
    public class InputDeck
    {
        public string name { get; set; }
        public string playerId { get; set; }
        public string[] cardIds { get; set; }
    }


    public class OutputDeck
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}