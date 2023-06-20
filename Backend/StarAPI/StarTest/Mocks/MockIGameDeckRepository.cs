using Contracts;
using Moq;
using StarAPI.Models;

internal class MockIGameDeckRepository
{
    public static Mock<IGameDeckRepository> GetMock()
    {
        var mock = new Mock<IGameDeckRepository>();
        var gameDeck = new List<Game_Deck>()
        {
            new Game_Deck()
            {
                id = 1,
                cardId = "C-000000000001",
                playerId = "U-000000000001",
                gameId = "G-000000000001",
            },
            new Game_Deck()
            {
                id = 2,
                cardId = "C-000000000002",
                playerId = "U-000000000001",
                gameId = "G-000000000001",
            },
            new Game_Deck()
            {
                id = 3,
                cardId = "C-000000000003",
                playerId = "U-000000000002",
                gameId = "G-000000000001",
            },
            new Game_Deck()
            {
                id = 4,
                cardId = "C-000000000004",
                playerId = "U-000000000003",
                gameId = "G-000000000002",
            },
        };
        // Set up
      

        mock.Setup(m => m.Get(It.IsAny<int>()))
            .Returns((int id) => gameDeck.FirstOrDefault(ct => ct.id == id));
        
        mock.Setup(m => m.GetAll())
            .Returns(gameDeck);

        mock.Setup(m => m.Add(It.IsAny<Game_Deck>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<Game_Deck>()))
            .Callback(() => {return;});
        return mock;
    }

}