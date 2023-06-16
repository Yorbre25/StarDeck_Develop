
using Moq;
using StarAPI.DataHandling.Discovery;
using StarAPI.Models;

namespace StarTest;

public class CardTypeHandlingTest
{

    [Fact]    
    public void GetAllCardTypes_AllCardTypesReturn()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeHandling = new CardTypeHandling(repositoryWrapperMock.Object);

        // Act
        var result = cardTypeHandling.GetAllCardTypes();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count());
        Assert.IsType<List<CardType>>(result);
    }

    public void Get_ValidId_ReturnCardType()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeHandling = new CardTypeHandling(repositoryWrapperMock.Object);
        int testId = 1;

        // Act
        var result = cardTypeHandling.Get(testId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<CardType>(result);
        Assert.Equal("Básica", result);
    }

    [Fact]
    public void Get_InvalidId_ReturnNull()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeHandling = new CardTypeHandling(repositoryWrapperMock.Object);
        int testId = 9;

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => cardTypeHandling.Get(testId));
        Assert.Equal("Invalid CardType id", exception.Message);
    }

    [Fact]
    public void AddCardType_ValidCardType()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeHandling = new CardTypeHandling(repositoryWrapperMock.Object);
        string testCardType = "TestCardType";

        // Act
        cardTypeHandling.AddCardType(testCardType);

        // Assert
        repositoryWrapperMock.Verify(m => m.CardType.Add(It.IsAny<CardType>()), Times.Once());
        repositoryWrapperMock.Verify(m => m.Save(), Times.Once());
    }

    [Fact]
    public void AddCardType_ExistingCardType()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeHandling = new CardTypeHandling(repositoryWrapperMock.Object);
        string testCardType = "Básica";

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => cardTypeHandling.AddCardType(testCardType));
        Assert.Equal("CardType already exist", exception.Message);
    }

    [Fact]
    public void DeleteCardType_ValidId()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeHandling = new CardTypeHandling(repositoryWrapperMock.Object);
        int testId = 1;

        // Act
        cardTypeHandling.DeleteCardType(testId);

        // Assert
        repositoryWrapperMock.Verify(m => m.CardType.Delete(It.IsAny<CardType>()), Times.Once());
        repositoryWrapperMock.Verify(m => m.Save(), Times.Once());
    }

    [Fact]
    public void DeleteCardType_InvalidId()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeHandling = new CardTypeHandling(repositoryWrapperMock.Object);
        int testId = 9;

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => cardTypeHandling.DeleteCardType(testId));
        Assert.Equal("Invalid CardType id", exception.Message);
    }
}