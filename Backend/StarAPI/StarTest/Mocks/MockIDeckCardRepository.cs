using Contracts;
using Moq;
using StarAPI.Models;

internal class MockIDeckCardRepository
{
    public static Mock<IDeckCardRepository> GetMock()
    {
        var mock = new Mock<IDeckCardRepository>();
        var deckCard = new List<Deck_Card>()
        {
            new Deck_Card()
            {
                deckId = "D-000000000001",
                cardId = "C-000000000001",
            },
            new Deck_Card()
            {
                deckId = "D-000000000001",
                cardId = "C-000000000002",
            },
            new Deck_Card()
            {
                deckId = "D-000000000003",
                cardId = "C-000000000003",
            },
            new Deck_Card()
            {
                deckId = "D-000000000003",
                cardId = "C-000000000003",
            }
        };
        // Set up
      
        mock.Setup(m => m.GetAll())
            .Returns(deckCard);

        mock.Setup(m => m.Add(It.IsAny<Deck_Card>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<Deck_Card>()))
            .Callback(() => {return;});
        return mock;
    }

}