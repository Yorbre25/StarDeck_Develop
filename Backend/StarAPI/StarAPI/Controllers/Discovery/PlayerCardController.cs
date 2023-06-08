using Microsoft.AspNetCore.Mvc;
using StarAPI.DTO.Discovery;
using StarAPI.Context;
using StarAPI.Logic;
using StarAPI.DataHandling.Discovery;

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
        private ILogger<PlayerCardController> _logger;
        public PlayerCardController(StarDeckContext context, ILogger<PlayerCardController> logger)
        {
            this._cardPackageGenerator = new CardPackageGenerator(context);
            this._playerCardHandling = new PlayerCardHandling(context);
            this._newPlayerCardGenerator = new NewPlayerCardGenerator(context);
            this._logger = logger;
        }

        [HttpGet("CardCount/{playerId}")]
        public int GetCardCount(string playerId)
        {   
            return _playerCardHandling.GetCardCount(playerId);
        }
       

        [HttpPost("GenerateCardsForNewPlayer/{playerId}")]
        public ActionResult GenerateCardsForNewPlayer(string playerId)
        {
            try
            {
                _newPlayerCardGenerator.GenerateCardsForNewPlayer(playerId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Error assigning cards to new player");
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetPlayerCards/{playerId}")]
        public List<OutputCard> GetPlayerCards(string playerId) 
        {
            return _playerCardHandling.GetPlayerCards(playerId);
        }

        [HttpGet("GetPackagesForNewPlayer")]
        public List<List<OutputCard>> GetPackagesForNewPlayer() 
        {
            try
            {
                return this._cardPackageGenerator.GetPackagesForNewPlayer();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error getting packages for new player");
                return new List<List<OutputCard>>();
            }    
        }
        

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
                _logger.LogWarning("Error assigning card to player");
                return BadRequest();
            }
        }

    }
}
