using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{

    private readonly StarDeckDbContext _DBContext;


    public CardController(StarDeckDbContext dBContext)
    {
        this._DBContext = dBContext;
    }

    [HttpGet]
    [Route("GetAll")]
    public IActionResult Get()
    {
        var cards = this._DBContext.Cards.ToList();
        return Ok(cards);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public IActionResult Get(string id)
    {
        var card = this._DBContext.Cards.FirstOrDefault(x => x.Id == id);
        return Ok(card);
    }

    [HttpPost]
    [Route("Post")]
    public IActionResult Post([FromBody] Card _card)
    {
        var card = this._DBContext.Cards.FirstOrDefault(x => x.Id == _card.Id);
        if (card != null)
        {
            return BadRequest("Card already exists");
        }
        else
        {
            this._DBContext.Cards.Add(_card);
            this._DBContext.SaveChanges();
        }
        return Ok(true);
    }
}
