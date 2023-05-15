// using StarAPI.Models;
// using StarAPI.Context;
// using StarAPI.Utils;
// using StarAPI.Logic.ModelHandling;
// using StarAPI.DTOs;

// namespace StarAPI.Logic.GameLogic;

// public class TopGame : IGame{
//     private GamePlayer player1;
//     private GamePlayer player2;
//     private GameTable gameTable;
//     public SetupParam setupParameters { get; set; }


//     public TopGame(StarDeckContext _context)
//     {
//         this.player1 = new GamePlayer();
//         this.player2 = new GamePlayer();
//         this.gameTable = new GameTable(_context);
//     }

//     public DateTime Now
//     {
//         get { return DateTime.Now; }
//     }

//     public void SetupParam(SetupParam setupParam)
//     {
//         this.setupParameters = setupParam;
//     }

//     public List<OutputPlanet> SetupTable()
//     {
//         SetupPlanets();
//         return GetPlanets();
//     }

//     public List<OutputPlanet> GetPlanets()
//     {
//         return gameTable.planets;
//     }

//     public void SetupPlanets()
//     {
//         gameTable.SetupPlanets();
//     }

// }