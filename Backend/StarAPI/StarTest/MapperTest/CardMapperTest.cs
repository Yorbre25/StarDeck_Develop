
using Moq;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Models;

namespace StarTest;

public class CardMapperTest
{
    [Fact]
    public void FillOutputCard_OkCard_ReturnOutputCard()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardMapper = new CardMapper(repositoryWrapperMock.Object);
        Card card = new Card()
        {
            id = "C-000000000001",
            name = "TestCard",
            energy = 1,
            cost = 1,
            typeId = 1,
            raceId = 1,
            activatedCard = true,
            description = "TestDescription",
            imageId = 1,
        };

        // Act
        var result = cardMapper.FillOutputCard(card);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<OutputCard>(result);
        Assert.Equal(card.id, result.id);
        Assert.Equal(card.name, result.name);
        Assert.Equal("Básica", result.type);
        Assert.Equal(card.description, result.description);
        Assert.Equal("Image 1", result.image);
    }
}
