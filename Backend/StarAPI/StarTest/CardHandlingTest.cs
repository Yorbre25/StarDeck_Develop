using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarAPI.Controllers;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Models;

namespace StarTest;

public class CardHandlingTest
{

    [Fact]    
    public void GetCard_ValidId_OutputCard()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardHandling = new CardHandling(repositoryWrapperMock.Object);
        string cardId = "C-000000000001";

        // Act
        var result = cardHandling.GetCard(cardId);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<OutputCard>(result);
    }

    [Fact]    
    public void GetCard_InvalidId_NullReferenceException()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardHandling = new CardHandling(repositoryWrapperMock.Object);
        string cardId = "C-0000000000f234";

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => cardHandling.GetCard(cardId));
    }

    [Fact]    
    public void GetAllCards()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardHandling = new CardHandling(repositoryWrapperMock.Object);

        // Act
        var result = cardHandling.GetAllCards();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<OutputCard>>(result);
        Assert.Equal(3, result.Count);
    }
    [Fact]    
    public void AddCard_ValidInputCard()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardHandling = new CardHandling(repositoryWrapperMock.Object);
        InputCard inputCard = new InputCard() 
        {
            name = "TestCard",
            energy = 1,
            cost = 1,
            typeId = 1,
            raceId = 1,
            description = "TestDescription",
            image = "Image 1",
        };

        // Act
        cardHandling.AddCard(inputCard);

        // Assert
        repositoryWrapperMock.Verify(m => m.Card.Add(It.IsAny<Card>()), Times.Once());
    }
    [Fact]    
    public void DeleteCard_ValidCardId()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardHandling = new CardHandling(repositoryWrapperMock.Object);
        string cardId = "C-000000000001";

        // Act
        cardHandling.DeleteCard(cardId);

        // Assert
        repositoryWrapperMock.Verify(m => m.Card.Get(cardId), Times.Once());
        repositoryWrapperMock.Verify(m => m.Card.Delete(It.IsAny<Card>()), Times.Once());
        repositoryWrapperMock.Verify(m => m.Save(), Times.Once());

    }
    [Fact]
    public void GetCardsByType_ValidCardType()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardHandling = new CardHandling(repositoryWrapperMock.Object);
        string cardTypeName = "Básico";

        // Act
        var result = cardHandling.GetCardsByType(cardTypeName);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<OutputCard>>(result);
        repositoryWrapperMock.Verify(m => m.Card.GetAll(), Times.Once());

    }
}