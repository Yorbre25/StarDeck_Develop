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

        [HttpPost("EndTurn")]
        public ActionResult EndTurn([FromBody] InputTableLayout tableLayout)
        {
            try{
                //Verficación de botones y no pasar el turno instantaneamente
                //Si queda tiempo implmentarlo por tiempo
                _gameLogic.EndTurn(tableLayout);
                return Ok();
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetLayout/{gameId}/{playerId}")]
        public ActionResult GetLayout(string gameId, string playerId)
        {
            try
            {
                return Ok(_gameLogic.GetLayout(gameId, playerId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetTurnInfo/{gameId}/{playerId}")]
        public ActionResult GetTurnInfo(string gameId, string playerId)
        {
            try
            {
                return Ok(_gameLogic.GetTurnInfo(gameId, playerId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}