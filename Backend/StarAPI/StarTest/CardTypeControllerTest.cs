using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarAPI.Controllers;
using StarAPI.DataHandling.Discovery;
using StarAPI.Models;

namespace StarTest;

public class CardTypeControllerTest
{

    [Fact]    
    public void GetAllCardTypes_ReturnAllCardType()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeController = new CardTypeController(repositoryWrapperMock.Object);

        // Act
        var result = cardTypeController.GetAllCardTypes() as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.IsAssignableFrom<List<CardType>>(result.Value);
    }

    [Fact]    
    public void Get_ValidId()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeController = new CardTypeController(repositoryWrapperMock.Object);
        int id = 1;

        // Act
        var result = cardTypeController.GetCardType(id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.IsAssignableFrom<string>(result.Value);
        Assert.Equal("Básica", result.Value);
    }

    [Fact]
    public void Get_InvalidId()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        var cardTypeController = new CardTypeController(repositoryWrapperMock.Object);
        int id = 9;

        // Act
        var result = cardTypeController.GetCardType(id) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
    }

}