using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarAPI.Controllers;
using StarAPI.DataHandling.Discovery;
using StarAPI.DataHandling.Game;
using StarAPI.Models;

namespace StarTest;

public class GameDeckHandlingTest
{

    [Fact]    
    public void GetGameDeckByGameId_ValidGameId_ReturnGameDeckList()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var gameDeckHandling = new GameDeckHandling(repositoryWrapperMock.Object);
        var gameId = "G-000000000001";
        
        // Act
        var result = gameDeckHandling.GetGameDeckByGameId(gameId);

        // Assert
        Assert.NotEmpty(result);
        Assert.IsAssignableFrom<List<Game_Deck>>(result);
        repositoryWrapperMock.Verify(m => m.GameDeck.GetAll(), Times.Once());
    }
    [Fact]    
    public void GetGameDeckByGameId_InvalidGameId_ReturnEmptyList()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var gameDeckHandling = new GameDeckHandling(repositoryWrapperMock.Object);
        var gameId = "G-000000000jsd";
        
        // Act
        var result = gameDeckHandling.GetGameDeckByGameId(gameId);

        // Assert
        Assert.Empty(result);
        Assert.IsAssignableFrom<List<Game_Deck>>(result);
        repositoryWrapperMock.Verify(m => m.GameDeck.GetAll(), Times.Once());
    }

    [Fact]
    public void GetGameDeckByPlayerId_ValidPlayerId_ReturnGameDeckList()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var gameDeckHandling = new GameDeckHandling(repositoryWrapperMock.Object);
        var playerId = "U-000000000001";
        
        // Act
        var result = gameDeckHandling.GetGameDeckByPlayerId(playerId);

        // Assert
        Assert.NotEmpty(result);
        Assert.IsAssignableFrom<List<Game_Deck>>(result);
        repositoryWrapperMock.Verify(m => m.GameDeck.GetAll(), Times.Once());
    }
    [Fact]    
    public void GetGameDeckByPlayerId_InvalidPlayerId_ReturnEmptyList()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var gameDeckHandling = new GameDeckHandling(repositoryWrapperMock.Object);
        var playerId = "U-000000000jsd";
        
        // Act
        var result = gameDeckHandling.GetGameDeckByPlayerId(playerId);

        // Assert
        Assert.Empty(result);
        Assert.IsAssignableFrom<List<Game_Deck>>(result);
        repositoryWrapperMock.Verify(m => m.GameDeck.GetAll(), Times.Once());
    }

    [Fact]    
    public void RemoveCardFromDeck_ValidCard()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var gameDeckHandling = new GameDeckHandling(repositoryWrapperMock.Object);
        var playerId = "U-000000000001";
        var cardId = "C-000000000001";
        
        // Act
        gameDeckHandling.RemoveCardFromDeck(playerId, cardId);

        // Assert
        repositoryWrapperMock.Verify(m => m.GameDeck.Delete(It.IsAny<Game_Deck>()), Times.Once());
    }
 
    
}