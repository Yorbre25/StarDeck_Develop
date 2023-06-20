using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StarAPI.Controllers;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Game;
using StarAPI.Models;

namespace StarTest;

public class PlanetsForGameTest
{

    // [Fact]    
    // public void GetAvailbePlanets_ReturnAllCardType()
    // {
    //     // Arrange
    //     var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
    //     PlanetsForGame planetsForGame = new PlanetsForGame(repositoryWrapperMock.Object);

    //     // Act
    //     planetsForGame.GetAvaiblePlanets();

    //     // Assert
    //     repositoryWrapperMock.Verify(m => m.Planet.GetAll(), Times.Once());
    // }

    [Theory]    
    [InlineData(0, false)]
    [InlineData(2, false)]
    [InlineData(4, true)]
    [InlineData(-5, false)]
    public void EnoughtPlanets(int numPlanets, bool expected)
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        PlanetsForGame planetsForGame = new PlanetsForGame(repositoryWrapperMock.Object);

        // Act
        bool result = planetsForGame.EnoughtPlanets(numPlanets);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GeneratePlanets_AllRight_ReturnPlanets()
    {
        // Arrange
        var repositoryWrapperMock = MockRepositoryWrapper.GetMock();
        PlanetsForGame planetsForGame = new PlanetsForGame(repositoryWrapperMock.Object);
        planetsForGame.popularPlanets = new List<OutputPlanet>() {new OutputPlanet(){}};
        planetsForGame.basicPlanets = new List<OutputPlanet>() {new OutputPlanet(){}};
        planetsForGame.rarePlanets =new List<OutputPlanet>() {new OutputPlanet(){}};

        // Act
        var result = planetsForGame.GeneratePlanets();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<OutputPlanet>>(result);
        Assert.Equal(3, result.Count());
    }

}