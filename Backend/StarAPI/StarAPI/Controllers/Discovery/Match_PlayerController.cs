using Contracts;
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

    public Match_PlayerController(IRepositoryWrapper repository)
    {
        cancel = CancelRequest.Instance;
        matchmaking = new Matchmaking(repository);


        
    }

    [HttpGet("{id}/{deckId}")]
    public async Task<ActionResult> LongRunningMethod(string id, string deckId)
    {
        cancel.start = true;

        
        if(!matchmaking.match(id, deckId))
        {

            
            //return StatusCode((int)HttpStatusCode.RequestTimeout, "Operation cancelled by user");


            return BadRequest("Something went wrong");

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
