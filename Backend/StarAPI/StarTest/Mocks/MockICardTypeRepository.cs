using Contracts;
using Moq;
using StarAPI.Models;

internal class MockICardTypeRepository
{
    public static Mock<ICardTypeRepository> GetMock()
    {
        var mock = new Mock<ICardTypeRepository>();
        var cardTypes = new List<CardType>()
        {
            new CardType()
            {
                id = 1,
                typeName = "Básica",
            },
            new CardType()
            {
                id = 2,
                typeName = "Normal",
            },
            new CardType()
            {
                id = 3,
                typeName = "Rara",
            },
            new CardType()
            {
                id = 4,
                typeName = "Muy Rara",
            },
            new CardType()
            {
                id = 5,
                typeName = "Ultra Rara",
            }
        };
        // Set up
      

        mock.Setup(m => m.Get(It.IsAny<int>()))
            .Returns((int id) => cardTypes.FirstOrDefault(ct => ct.id == id));
        
        mock.Setup(m => m.GetAll())
            .Returns(cardTypes);

        mock.Setup(m => m.Add(It.IsAny<CardType>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<CardType>()))
            .Callback(() => {return;});
        return mock;
    }

}