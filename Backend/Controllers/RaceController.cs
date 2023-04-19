using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class RaceController : ControllerBase
{

    private readonly StarDeckDbContext _DBContext;


    public RaceController(StarDeckDbContext dBContext)
    {
        this._DBContext = dBContext;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public dynamic Get()
    {
        var races = this._DBContext.Races.ToList();
        return Ok(races);
    }

    [HttpPost]
    [Route("Post")]
    public dynamic Post([FromBody] Race _race)
    {
        //* Regular code

        // var race = this._DBContext.Races.FirstOrDefault(x => x.Id == _race.Id);
        // if (race != null)
        // {
        //     return BadRequest("Card already exists");
        // }
        // else
        // {
        //     this._DBContext.Races.Add(_race);
        //     this._DBContext.SaveChanges();
        // }
        return Ok(true);

        //* Add test

        // var races = new List<Race>
        // {
        //     new Race {Id = 1, RaceName = "Ogro"},
        //     new Race {Id = 2, RaceName = "Humano"},
        //     new Race {Id = 4, RaceName = "Elfo"},
        //     new Race {Id = 3, RaceName = "Angel"}
        // };

        //     this._DBContext.Races.Add(races)
        //     this._DBContext.SaveChanges();
        // };

        
    }
}
