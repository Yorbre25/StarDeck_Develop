using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StarAPI.Context;
using StarAPI.Controllers;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StarTest
{
    public class TurnTest
    {
        private SqliteConnection _connection;
        private readonly DbContextOptions<StarDeckContext> _options;
        public TurnTest()
        {
            _connection = new SqliteConnection("Data source = C:/Users/Nasser/Documents/Espe/Backend/StarAPI/StarAPI/Context/StarDeck.db");
            _connection.Open();
            _options = new DbContextOptionsBuilder<StarDeckContext>().UseSqlite(_connection).Options;
            using (var context = new StarDeckContext(_options)) { context.Database.EnsureCreated(); }
        }

        [Fact]
        public void isOk() 
        {
            using (var context = new StarDeckContext(_options)) 
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });
                ILogger<TurnController> logger = loggerFactory.CreateLogger<TurnController>();
                TurnController turnController = new TurnController(context, logger);
                Game game = context.Game.FirstOrDefault();
                Assert.NotNull(game);
                var result = turnController.DrawCard(game.id, game.player1Id);

                OutputCard card;
                Dictionary<string, string> playerCards = new Dictionary<string, string>();
                if (result is OkObjectResult okObjectResult)
                {
                    card = (OutputCard) okObjectResult.Value;
                    var planetId = context.Game_Planet.FirstOrDefault().planetId;
                    playerCards.Add(planetId, card.id);
                    

                }
                
                InputTableLayout layout = new InputTableLayout() { gameId = game.id, playerId = game.player1Id, layout = playerCards };
                var Result = turnController.EndTurn(layout);

                Assert.IsType<BadRequestResult>(Result);

            }
        }
    }
}
