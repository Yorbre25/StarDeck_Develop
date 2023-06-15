using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Context;
using StarAPI.Logic;
using Contracts;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IRepositoryWrapper _reposiory;
        private CardCRUD _cardCrud;

        public CardController(IRepositoryWrapper repository)
        {
            this._reposiory = repository;
            this._cardCrud = new CardCRUD(_reposiory);
        }


 

        [HttpGet("GetAllCards")]
        public IEnumerable<OutputCard> GetAllCards()
        {
            // _reposiory.Database.ExecuteSqlRaw("DELETE FROM Game_Player");
            return _cardCrud.GetAllCards();
        }



        [HttpGet("GetCardById/{id}")]
        public OutputCard GetCard(string id)
        {
            return _cardCrud.GetCard(id);
        }


        [HttpPost("AddCard")]
        public ActionResult Post([FromBody] InputCard card)
        {
            try
            {
                _cardCrud.AddCard(card);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddCards")]
        public ActionResult Post([FromBody] List<Card> cards)
        {
            try
            {
                foreach(var card in cards) 
                {
                    _reposiory.Card.Add(card);
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteCard/{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _cardCrud.DeleteCard(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
