using StarAPI.DTO.Discovery;
using StarAPI.Logic;
using StarAPI.Models;

namespace StarTest
{
    public class NewTableTest
    {
        // public LoginTest() 
        // {
        //     _connection = new SqliteConnection("Data source = C:/Users/Nasser/Documents/Espe/Backend/StarAPI/StarAPI/Context/StarDeck.db");
        //     _connection.Open();
        //     _options = new DbContextOptionsBuilder<StarDeckContext>().UseSqlite(_connection).Options;
        //     using (var context = new StarDeckContext(_options)) { context.Database.EnsureCreated(); }


        // }
        [Fact]
        public void SetGamePlanetValues_ProperInputs_GamePlanetList()
        {
            //Arrange
            // string gameId = "G-000000000001";
            // OutputPlanet planet1 = new OutputPlanet() { id="P-000000000001", name="Planet1", type="type1", description="description1", image="image1", show=true };
            // OutputPlanet planet2 = new OutputPlanet() { id="P-000000000002", name="Planet2", type="type2", description="description2", image="image2", show=true };
            // List<OutputPlanet> planets = new List<OutputPlanet>() { planet1, planet2 };

            // NewTable newTable = new NewTable();
            // //Act
            // List<Game_Planet> gamePlanetList = newTable.SetGamePlanetValues(gameId, planets);

            // //Assert
            // Assert.Equal(gameId, gamePlanetList[0].gameId);
            // Assert.Equal(planet1.id, gamePlanetList[0].planetId);
            // Assert.True(gamePlanetList[0].show);

            // Assert.Equal(gameId, gamePlanetList[1].gameId);
            // Assert.Equal(planet2.id, gamePlanetList[1].planetId);
            // Assert.True(gamePlanetList[1].show);
        }
    }
}