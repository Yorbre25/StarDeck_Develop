using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using StarAPI.Context;
using StarAPI.Logic.Login;
using StarAPI.Logic.Match;
using StarAPI.Models;
using System.Threading.Tasks;

namespace StarTest
{
    public class MatchTest
    {
        private SqliteConnection _connection;
        private readonly DbContextOptions<StarDeckContext> _options;

        public MatchTest() 
        {
            _connection = new SqliteConnection("Data source = C:/Users/Nasser/Documents/Espe/Backend/StarAPI/StarAPI/Context/StarDeck.db");
            _connection.Open();
            _options = new DbContextOptionsBuilder<StarDeckContext>().UseSqlite(_connection).Options;
            using (var context = new StarDeckContext(_options)) { context.Database.EnsureCreated(); }
        }

        
        public async Task IsBad()
        {
            _default();
            using (var context = new StarDeckContext(_options)) 
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>{});         
                ILogger<Match_PlayerController> logger = loggerFactory.CreateLogger<Match_PlayerController>();

                Match_PlayerController matchmaking = new Match_PlayerController(context, logger);
                var mp1 = await matchmaking.LongRunningMethod("U-i1reg2ikofvz", "D-ums69tjm32d7");
          
                Assert.IsType<BadRequestObjectResult>(mp1);
            }
        }

        [Fact]
        public async Task IsOk()
        {
            _default();
            using (var context = new StarDeckContext(_options))
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });
                ILogger<Match_PlayerController> logger = loggerFactory.CreateLogger<Match_PlayerController>();

                Match_PlayerController matchmaking = new Match_PlayerController(context,logger);
                var mp = new Match_Player();
                mp.id = "U-i1reg2ikofvz";
                mp.deckId = "D-ums69tjm32d7";
                mp.waiting_since = DateTime.Now;
                context.Match_Player.Add(mp);
                var mp1 = await matchmaking.LongRunningMethod("U-71r3cqn5d7ks", "D-9ip894944xrk");
                
                Assert.IsType<OkResult>(mp1);
            }
            
        }

        public void _default() 
        {
            using (var context = new StarDeckContext(_options)) 
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Match_Player");
                context.Database.ExecuteSqlRaw("DELETE FROM Game WHERE(Game.id != 'G-w0cptj6dwecv');");
                context.Database.ExecuteSqlRaw("DELETE FROM Game_Player WHERE(Game_Player.gameId != 'G-w0cptj6dwecv');");
                context.Database.ExecuteSqlRaw("DELETE FROM Game_Deck WHERE(Game_Deck.gameId != 'G-w0cptj6dwecv');");
                context.Database.ExecuteSqlRaw("DELETE FROM Game_Planet WHERE(Game_Planet.gameId != 'G-w0cptj6dwecv');");
                context.SaveChanges();
            }
        }
    }
}
