using Contracts;
using Moq;
using StarAPI.Models;

internal class MockIHandRepository
{
    public static Mock<IHandRepository> GetMock()
    {
        var mock = new Mock<IHandRepository>();
        var hand = new List<Hand>()
        {
            new Hand()
            {
                id = 1,
                cardId = "C-000000000001",
                playerId = "U-000000000001",
                gameId = "G-000000000001",
            },
            new Hand()
            {
                id = 2,
                cardId = "C-000000000002",
                playerId = "U-000000000001",
                gameId = "G-000000000001",
            },
            new Hand()
            {
                id = 3,
                cardId = "C-000000000003",
                playerId = "U-000000000002",
                gameId = "G-000000000001",
            },
            new Hand()
            {
                id = 4,
                cardId = "C-000000000004",
                playerId = "U-000000000003",
                gameId = "G-000000000002",
            },
        };
        // Set up
      

        mock.Setup(m => m.Get(It.IsAny<int>()))
            .Returns((int id) => hand.FirstOrDefault(ct => ct.id == id));
        
        mock.Setup(m => m.GetAll())
            .Returns(hand);

        mock.Setup(m => m.Add(It.IsAny<Hand>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<Hand>()))
            .Callback(() => {return;});
        return mock;
    }

}