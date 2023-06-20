using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarAPI.Controllers;
using StarAPI.DataHandling.Discovery;
using StarAPI.DataHandling.Game;
using StarAPI.DTO.Discovery;
using StarAPI.Models;

namespace StarTest;

public class HandHandlingTest
{

    [Fact]    
    public void GetHandCardsByPlayerId_ValidPlayerId_ReturnHandList()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var handHandling = new HandCard(repositoryWrapperMock.Object);
        var playerId = "U-000000000001";
        
        // Act
        var result = handHandling.GetHandCardsByPlayerId(playerId);

        // Assert
        Assert.NotEmpty(result);
        Assert.IsAssignableFrom<List<OutputCard>>(result);
        repositoryWrapperMock.Verify(m => m.Hand.GetAll(), Times.Once());
    }

    [Fact]    
    public void RemoveCardsFromHand_ValidCard()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var handHandling = new HandCard(repositoryWrapperMock.Object);
        var playerId = "U-000000000001";
        var cardId = "C-000000000001";
        
        // Act
        handHandling.RemoveCardsFromHand(playerId, cardId);

        // Assert
        repositoryWrapperMock.Verify(m => m.Hand.Delete(It.IsAny<Hand>()), Times.Once());
    }
}