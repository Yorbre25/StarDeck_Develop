// using Microsoft.AspNetCore.Mvc;
// using StarAPI.Context;
// using StarAPI.DataTesting;
// namespace StarAPI.Controllers
// {
//     /// This class is used to fill the database with placeholder data.
//     [Route("[controller]")]
//     [ApiController]
//     public class PopulateController : ControllerBase
//     {
//         private readonly StarDeckContext _context;

//         private PopulateDB _populateDB;
//         private AddPlaceholderData _addPlaceholderData;

//         public PopulateController(StarDeckContext context)
//         {
//             this._context = context;
//             _populateDB = new PopulateDB(_context);
//             _addPlaceholderData = new AddPlaceholderData(_context);
//         }

       

//         [HttpPost]
//         [Route("PopulateDataBase")]
//         public ActionResult PopulateDB()
//         {
//             try
//             {
//                 _populateDB.Populate();
//                 return Ok();
//             }
//             catch (Exception e)
//             {
//                 return BadRequest(e.Message);
//             }
//         }

//         [HttpPost]
//         [Route("AddPlaceholderData")]
//         public ActionResult AddData()
//         {
//             try
//             {
//                 _addPlaceholderData.AddData();
//                 return Ok();
//             }
//             catch (Exception e)
//             {
//                 return BadRequest(e.Message);
//             }
//         }
//     }

// }
