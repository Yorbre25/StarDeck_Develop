// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using Moq;
// using StarAPI.Context;
// using StarAPI.Logic.Login;
// using StarAPI.Logic.Match;
// using StarAPI.Models;
// using System.Threading.Tasks;

// namespace StarTest
// {
//     public class MatchTest
//     {
//         private SqliteConnection _connection;
//         private readonly DbContextOptions<StarDeckContext> _options;

//         public MatchTest() 
//         {
//             _connection = new SqliteConnection("Data source = C:/Users/Nasser/Documents/Espe/Backend/StarAPI/StarAPI/Context/StarDeck.db");
//             _connection.Open();
//             _options = new DbContextOptionsBuilder<StarDeckContext>().UseSqlite(_connection).Options;
//             using (var context = new StarDeckContext(_options)) { context.Database.EnsureCreated(); }
//         }

//         [Fact]
//         public async Task IsBad()
//         {
//             _default();
//             using (var context = new StarDeckContext(_options)) 
//             {
//                 ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>{});         
//                 ILogger<Match_PlayerController> logger = loggerFactory.CreateLogger<Match_PlayerController>();

//                 Match_PlayerController matchmaking = new Match_PlayerController(context, logger);
//                 var mp1 = await matchmaking.LongRunningMethod("U-xsonq9gzc0co", "D-c4n1gggv3mzs");
          
//                 Assert.IsType<BadRequestObjectResult>(mp1);
//             }
//         }

//         [Fact]
//         public async Task IsOk()
//         {
//             _default();
//             using (var context = new StarDeckContext(_options))
//             {
//                 ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });
//                 ILogger<Match_PlayerController> logger = loggerFactory.CreateLogger<Match_PlayerController>();

//                 Match_PlayerController matchmaking = new Match_PlayerController(context,logger);
//                 var mp = new Match_Player();
//                 mp.id = "U-xsonq9gzc0co";
//                 mp.deckId = "D-c4n1gggv3mzs";
//                 mp.waiting_since = DateTime.Now;
//                 context.Match_Player.Add(mp);
//                 var mp1 = await matchmaking.LongRunningMethod("U-fvkc2dq9yza4", "D-uhdo33yy14v5");
                
//                 Assert.IsType<OkResult>(mp1);
//             }
            
//         }

//         public void _default() 
//         {
//             using (var context = new StarDeckContext(_options)) 
//             {
//                 context.Database.ExecuteSqlRaw("DELETE FROM Match_Player");
//                 context.Database.ExecuteSqlRaw("DELETE FROM Game");
//                 context.Database.ExecuteSqlRaw("DELETE FROM Game_Player");
//                 context.Database.ExecuteSqlRaw("DELETE FROM Game_Deck");
//                 context.Database.ExecuteSqlRaw("DELETE FROM Game_Planet");
//                 context.SaveChanges();
//             }
//         }
//     }
// }
