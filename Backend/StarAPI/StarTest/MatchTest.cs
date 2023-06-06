using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using StarAPI.Context;
using StarAPI.Logic.Login;
using StarAPI.Logic.Match;
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

        [Fact]
        public async Task IsOk()
        {
            using (var context = new StarDeckContext(_options)) 
            {
                Match_PlayerController matchmaking = new Match_PlayerController(context);
                Task mp1 =  matchmaking.LongRunningMethod("U-i1reg2ikofvz", "D-ums69tjm32d7");
          
                Assert.IsType<Task<IActionResult>>(mp1);
            }
        }
        
    }
}
