using Microsoft.AspNetCore.Mvc;
using StarAPI.DTOs;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic;
using StarAPI.Logic.ModelHandling;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerCardController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private PlayerCardHandling _playerCardHandling;
        private CardPackageGenerator _cardPackageGenerator;
        private NewPlayerCardGenerator _newPlayerCardGenerator;
        public PlayerCardController(StarDeckContext context)
        {
            this._context = context;
            this._cardPackageGenerator = new CardPackageGenerator(context);
            this._playerCardHandling = new PlayerCardHandling(context);
            this._newPlayerCardGenerator = new NewPlayerCardGenerator(context);
        }

        [HttpGet("CardCount/{playerId}")]
        public int GetCardCount(string playerId)
        {   
            return _playerCardHandling.GetCardCount(playerId);
        }
       

        // [HttpPost("GenerateCardsForNewPlayer")]
        // public ActionResult GenerateCardsForNewPlayer([FromBody] string playerId)
        // {
        //     try
        //     {
        //         _newPlayerCardGenerator.GenerateCardsForNewPlayer(playerId);
        //         return Ok();
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        [HttpGet("GetPlayerCards/{playerId}")]
        public IEnumerable<OutputCard> GetPlayerCards(string playerId) 
        {
            return _playerCardHandling.GetPlayerCards(playerId);
        }

        // [HttpGet("GetPackagesForNewPlayer")]
        // public List<List<OutputCard>> GetPackagesForNewPlayer() 
        // {
        //     try
        //     {
        //         return this._cardPackageGenerator.GetPackagesForNewPlayer();
        //     }
        //     catch (Exception e)
        //     {
        //         return new List<List<OutputCard>>();
        //     }    
        // }
        

        [HttpPost("AssignCardToPlayer/{playerId}/{cardId}")]
        public ActionResult AssignCardToPlayer(string playerId, string cardId)
        {
            try
            {
                _playerCardHandling.AssignCard(playerId, cardId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

    }
}
