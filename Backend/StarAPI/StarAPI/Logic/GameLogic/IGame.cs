using StarAPI.DTOs;
using StarAPI.Models;

public interface IGame
{
    DateTime Now { get; }
    SetupParam setupParameters { get; set; }

    List<OutputPlanet> GetPlanets();

    // object? GetGameParam();
    void SetupParam(StarAPI.Models.SetupParam setupParam);
    List<OutputPlanet> SetupTable();
}