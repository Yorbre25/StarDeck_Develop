using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers.Game
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerTurnController : ControllerBase
    {
        private readonly StarDeckContext context;
        public PlayerTurnController(StarDeckContext context)
        {
            this.context = context;
        }

        // POST api/<PlayerTurn>
        [HttpPost]
        public ActionResult Post([FromBody] TurnPlayer turnPlayer)
        {
            try 
            {
                context.TurnPlayer.Add(turnPlayer);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<PlayerTurn>/5
        [HttpPut("{id}/{inTurn}")]
        public ActionResult Put(string id,bool inTurn)
        {
            var turnPlayer = context.TurnPlayer.FirstOrDefault(t=> t.playerId == id);
            if (turnPlayer != null) 
            {
                turnPlayer.inTurn = inTurn;
                context.Entry(turnPlayer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
            
        }

        // DELETE api/<PlayerTurn>/5
        [HttpDelete("{gameId}")]
        public ActionResult Delete(string gameId)
        {
            try 
            {
                var turnPlayers = context.TurnPlayer.ToList();
                turnPlayers = turnPlayers.FindAll(t => t.gameId == gameId).ToList();
                foreach (var turnPlayer in turnPlayers)
                {
                    context.TurnPlayer.Remove(turnPlayer);
                }
                context.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }
    }
}
