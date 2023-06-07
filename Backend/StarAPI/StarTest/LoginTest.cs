using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using StarAPI.Context;
using StarAPI.Logic.Login;
namespace StarTest
{
    public class LoginTest
    {
        private SqliteConnection _connection;
        private readonly DbContextOptions<StarDeckContext> _options;

        public LoginTest() 
        {
            _connection = new SqliteConnection("Data source = C:/Users/Nasser/Documents/Espe/Backend/StarAPI/StarAPI/Context/StarDeck.db");
            _connection.Open();
            _options = new DbContextOptionsBuilder<StarDeckContext>().UseSqlite(_connection).Options;
            using (var context = new StarDeckContext(_options)) { context.Database.EnsureCreated(); }


        }
        [Fact]
        public void IsOk()
        {

            using (var context = new StarDeckContext(_options))
            {
                var login = new Login(context);
                var result = login.Get("ye@donda.com","taylor12");
                Assert.IsType<OkResult>(result);
            }
        }
        
        [Fact]
        public void IsBad() 
        {
            using (var context = new StarDeckContext(_options))
            {
                var login = new Login(context);
                var result = login.Get("ye@donda.com", "taylor2");
                Assert.IsType<BadRequestResult>(result);
            }
        }
    }
}