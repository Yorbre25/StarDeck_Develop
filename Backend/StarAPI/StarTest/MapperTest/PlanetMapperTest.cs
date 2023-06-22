
using Moq;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Models;

namespace StarTest;

public class PlanetMapperTest
{
    [Fact]
    public void FillOutputPlanet_OkPlanet_ReturnOutputPlanet()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var planetMapper = new PlanetMapper(repositoryWrapperMock.Object);
        Planet planet = new Planet()
        {
            id = "P-000000000001",
            name = "TestPlanet",
            typeId = 1,
            activatedPlanet = true,
            description = "TestDescription",
            imageId = 1,
        };

        // Act
        var result = planetMapper.FillOutputPlanet(planet);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<OutputPlanet>(result);
        Assert.Equal(planet.id, result.id);
        Assert.Equal(planet.name, result.name);
        Assert.Equal("Popular", result.type);
        Assert.Equal(planet.description, result.description);
        Assert.Equal("Image 1", result.image);
    }
  
    [Fact]
    public void FillNewPlanet_OkInputPlanet_ReturnPlanet()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var planetMapper = new PlanetMapper(repositoryWrapperMock.Object);
        InputPlanet inputPlanet = new InputPlanet()
        {
            name = "TestPlanet",
            typeId = 1,
            description = "TestDescription",
            image = "Image 1",
        };
        string id = "P-000000000001";

        // Act
        var result = planetMapper.FillNewPlanet(inputPlanet, id);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Planet>(result);
        Assert.Equal(id, result.id);
        Assert.Equal(inputPlanet.name, result.name);
        Assert.Equal(inputPlanet.typeId, result.typeId);
        Assert.Equal(true, result.activatedPlanet);
        Assert.Equal(inputPlanet.description, result.description);
        Assert.Equal(1, result.imageId);
    }

}
