using Contracts;
using Moq;
using StarAPI.Models;

internal class MockICardRepository
{
    public static Mock<ICardRepository> GetMock()
    {
        var mock = new Mock<ICardRepository>();
        var cards = new List<Card>()
        {
            new Card()
            {
                id = "C-000000000001",
                name = "Test Card 1",
                energy = 1,
                cost = 1,
                typeId = 1,
                raceId = 1,
                activatedCard = true,
                description = "Test Card 1",
                imageId = 1,
            },
            new Card()
            {
                id = "C-000000000002",
                name = "Test Card 2",
                energy = 1,
                cost = 1,
                typeId = 1,
                raceId = 1,
                activatedCard = true,
                description = "Test Card 1",
                imageId = 1,
            },
            new Card()
            {
                id = "C-000000000001",
                name = "Test Card 2",
                energy = 1,
                cost = 1,
                typeId = 1,
                raceId = 1,
                activatedCard = true,
                description = "Test Card 1",
                imageId = 1,
            },
        };
        // Set up
        mock.Setup(m => m.Get(It.IsAny<string>()))
        .Returns((string id) => cards.FirstOrDefault(ct => ct.id == id));
        
        mock.Setup(m => m.GetAll())
            .Returns(cards);

        mock.Setup(m => m.Add(It.IsAny<Card>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<Card>()))
            .Callback(() => {return;});
        return mock;
    }

}