using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Logic.Match;
using StarAPI.Logic.Utils;
using System.Net;
using System.Threading;

public class Match_PlayerController : ControllerBase
{
    private CancelRequest cancel;
    private Matchmaking matchmaking;
    private readonly StarDeckContext _context;

    public Match_PlayerController(StarDeckContext context)
    {
        cancel = CancelRequest.Instance;
        matchmaking = new Matchmaking(context);


        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> LongRunningMethod(string id)
    {
        cancel.start = true;

        
        if(!matchmaking.match(id))
        {

            return StatusCode((int)HttpStatusCode.RequestTimeout, "Operation cancelled by user");
        }
            
       
        return Ok();

    }

    [HttpGet("cancel-long-running")]
    public IActionResult CancelLongRunningMethod()
    {
        if (cancel.start)
        {
            cancel.terminate = true;
            cancel.start = false;
            return Ok("Operation cancelled");
        }

        return BadRequest("No operation to cancel");
    }
}
