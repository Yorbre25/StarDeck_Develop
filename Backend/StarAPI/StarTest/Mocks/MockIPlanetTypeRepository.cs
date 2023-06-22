using Contracts;
using Moq;
using StarAPI.Models;

internal class MockIPlanetTypeRepository
{
    public static Mock<IPlanetTypeRepository> GetMock()
    {
        var mock = new Mock<IPlanetTypeRepository>();
        var planetTypes = new List<PlanetType>()
        {
            new PlanetType()
            {
                id = 2,
                typeName = "Basico",
            },
            new PlanetType()
            {
                id = 1,
                typeName = "Popular",
            },
            new PlanetType()
            {
                id = 3,
                typeName = "Rara",
            }
        };
        // Set up
      

        mock.Setup(m => m.Get(It.IsAny<int>()))
            .Returns((int id) => planetTypes.FirstOrDefault(ct => ct.id == id));
        
        mock.Setup(m => m.GetAll())
            .Returns(planetTypes);

        mock.Setup(m => m.Add(It.IsAny<PlanetType>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<PlanetType>()))
            .Callback(() => {return;});
        return mock;
    }

}