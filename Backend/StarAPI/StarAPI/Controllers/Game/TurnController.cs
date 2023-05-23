using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;
using StarAPI.Logic.Game;

namespace StarAPI.Controllers
{

    [ApiController]
    public class TurnController : ControllerBase
    {
        private GameLogic _gameLogic;
        private TableLogic _tableLogic;

        public TurnController(StarDeckContext context)
        {
            this._gameLogic = new GameLogic(context);
            this._tableLogic = new TableLogic(context);
        }


        [HttpPost("DrawCard/{gameId}/{playerId}")]
        public ActionResult DrawCard(string gameId, string playerId)
        {
            try
            {
                OutputCard outputCard = _gameLogic.DrawCard(gameId, playerId);
                if (outputCard == null)
                {
                    return Ok();
                }
                return Ok(outputCard);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("PlaceCard")]
        public ActionResult PlaceCard([FromBody] InputPlaceCard inputPlaceCard)
        {
            try
            {
                this._tableLogic.PlaceCard(inputPlaceCard);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("EndTurn")]
        public ActionResult EndTurn()
        {
            throw new NotImplementedException();
            //Verficación de botones
            //Aumentar puntos de colocar cartas
            //Regenerar puntos de colocar cartas
            //Mostrar Planeta Oculto en el turno 3
            //Si queda tiempo implmentarlo por tiempo
        }
    }
}