using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;
using StarAPI.Logic.Game;
using StarAPI.Logic;
using Contracts;

namespace StarAPI.Controllers
{

    [ApiController]
    public class TurnController : ControllerBase
    {
        private IRepositoryWrapper _repository;

        public TurnController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }


        [HttpPost("DrawCard/{gameId}/{playerId}")]
        public ActionResult DrawCard(string gameId, string playerId)
        {
            try
            {
                DrawCard drawCard = new DrawCard(_repository);
                return Ok(drawCard.Draw(gameId, playerId));
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
                EndTurn endTurn = new EndTurn(_repository);
                endTurn.End(tableLayout);
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
                TableLayout tableLayout = new TableLayout(_repository);
                return Ok(tableLayout.GetLayout(gameId, playerId));
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
                Turn turn = new Turn(_repository);
                return Ok(turn.GetTurnInfo(gameId, playerId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}