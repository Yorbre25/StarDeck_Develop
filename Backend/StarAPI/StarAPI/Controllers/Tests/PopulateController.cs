using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.AdminLogic;

namespace StarAPI.Controllers
{
    /// <summary>
    /// This class is used to handle all requests to the Card_Type table.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PopulateController : ControllerBase
    {
        private readonly StarDeckContext _context;

        private PopulateDB _populateDB;

        /// <summary>
        /// Constructor for PopulateController.
        /// </summary>
        /// <param name="context"></param>
        public PopulateController(StarDeckContext context)
        {
            this._context = context;
            _populateDB = new PopulateDB(_context);
        }

       

        [HttpPost]
        [Route("PopulateDataBase")]
        public ActionResult PopulateDB()
        {
            try
            {
                _populateDB.Populate();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }

}
