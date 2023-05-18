using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Logic.Match;
using StarAPI.Logic.Utils;
using System.Net;
using System.Threading;

[Route("[controller]")]
[ApiController]

public class Match_PlayerController : ControllerBase
{
    private CancelRequest cancel;
    private Matchmaking matchmaking;

    public Match_PlayerController(StarDeckContext context)
    {
        cancel = CancelRequest.Instance;
        matchmaking = new Matchmaking(context);


        
    }

    [HttpGet("{id}/{deckId}")]
    public async Task<IActionResult> LongRunningMethod(string id, string deckId)
    {
        cancel.start = true;

        
        if(!matchmaking.match(id, deckId))
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
