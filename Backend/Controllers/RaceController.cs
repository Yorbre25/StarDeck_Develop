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
    
    [HttpGet("GetAll")]
    public IActionResult Get()
    {
        var races = this._DBContext.Races.ToList();
        return Ok(races);
    }
}
