using Contracts;
using Moq;
using StarAPI.Models;

internal class MockIRaceRepository
{
    public static Mock<IRaceRepository> GetMock()
    {
        var mock = new Mock<IRaceRepository>();
        var races = new List<Race>()
        {
            new Race()
            {
                id = 1,
                name = "Humano",
            },
            new Race()
            {
                id = 2,
                name = "Trisolariano",
            },
            new Race()
            {
                id = 3,
                name = "Cyborg",
            },
            new Race()
            {
                id = 4,
                name = "Nocino",
            },
            new Race()
            {
                id = 5,
                name = "Presidente",
            }
        };
        // Set up
      

        mock.Setup(m => m.Get(It.IsAny<int>()))
            .Returns((int id) => races.FirstOrDefault(ct => ct.id == id));
        
        mock.Setup(m => m.GetAll())
            .Returns(races);

        mock.Setup(m => m.Add(It.IsAny<Race>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<Race>()))
            .Callback(() => {return;});
        return mock;
    }

}