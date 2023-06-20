using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarAPI.Controllers;
using StarAPI.DataHandling.Discovery;
using StarAPI.DataHandling.Game;
using StarAPI.DTO.Discovery;
using StarAPI.Models;

namespace StarTest;

public class DeckCardHandlingTest
{

    [Fact]    
    public void GetCards_ValidGameId_ReturnOutputCards()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var deckCardHandling = new DeckCardHandling(repositoryWrapperMock.Object);
        string[] cardIds = {"C-000000000001", "C-000000000002"};
        
        // Act
        var result = deckCardHandling.GetCards(cardIds);

        // Assert
        Assert.NotEmpty(result);
        Assert.IsAssignableFrom<List<OutputCard>>(result);
        Assert.Equal(2, result.Count);
    }
    // [Fact]    
    // public void GetCards_InvalidGameId_ReturnEmptyList()
    // {
    //     // Arrange
    //     var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
    //     var deckCardHandling = new DeckCardHandling(repositoryWrapperMock.Object);
    //     var gameId = "G-000000000jsd";
        
    //     // Act
    //     var result = deckCardHandling.GetCards(gameId);

    //     // Assert
    //     Assert.Empty(result);
    //     Assert.IsAssignableFrom<List<Game_Deck>>(result);
    //     repositoryWrapperMock.Verify(m => m.GameDeck.GetAll(), Times.Once());
    // }
}
