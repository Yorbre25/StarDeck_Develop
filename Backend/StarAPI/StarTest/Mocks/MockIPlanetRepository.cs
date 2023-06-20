using Contracts;
using Moq;
using StarAPI.Models;

internal class MockIPlanetRepository
{
    public static Mock<IPlanetRepository> GetMock()
    {
        var mock = new Mock<IPlanetRepository>();
        var planets = new List<Planet>()
        {
            new Planet()
            {
                id = "P-000000000001",
                name = "TestPlanet1",
                typeId = 1,
                activatedPlanet = true,
                description = "TestDescription1",
                imageId = 1
            },
            new Planet()
            {
                id = "P-000000000002",
                name = "TestPlanet2",
                typeId = 2,
                activatedPlanet = true,
                description = "TestDescription2",
                imageId = 1
            },
            new Planet()
            {
                id = "P-000000000003",
                name = "TestPlanet2",
                typeId = 3,
                activatedPlanet = true,
                description = "TestDescription2",
                imageId = 1
            },
            new Planet()
            {
                id = "P-000000000004",
                name = "TestPlanet2",
                typeId = 4,
                activatedPlanet = true,
                description = "TestDescription2",
                imageId = 1
            },
           
        };
        // Set up
      

        mock.Setup(m => m.Get(It.IsAny<string>()))
            .Returns((string id) => planets.FirstOrDefault(ct => ct.id == id));
        
        mock.Setup(m => m.GetAll())
            .Returns(planets);

        mock.Setup(m => m.Add(It.IsAny<Planet>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<Planet>()))
            .Callback(() => {return;});
        return mock;
    }

}