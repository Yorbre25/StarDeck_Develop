
namespace StarAPI.DTO.Game
{

    public class SetUpValues
    {
        public string player1Id { get; set; }
        public string player2Id { get; set; }
        public string player1DeckId { get; set; }
        public string player2DeckId { get; set; }

    }

    public class OutputSetupValues
    {
            public string id { get; set; }
            public int totalTurns { get; set; }
            public int timePerTurn { get; set; }
            public int currentTurn { get; set; }
            public string player1Id { get; set; }
            public string player2Id { get; set; }
            public string usernamePlayer1 { get; set; }
            public string usernamePlayer2 { get; set; }
            public string deckNamePlayer1 { get; set; }
            public string deckNamePlayer2 { get; set; }
    }
}