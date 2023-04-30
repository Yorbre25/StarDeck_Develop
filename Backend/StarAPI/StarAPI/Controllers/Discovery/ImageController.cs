using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using System;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly StarDeckContext context;

        public ImageController(StarDeckContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Card_Image> GetAllImages()
        {
            return context.Card_Image.ToList();
        }

        // GET api/<DeckController>/5
        [HttpPost("{image_string}")]
        public ActionResult AddImage([FromBody] Card_Image card_image)
        {
            context.Card_Image.Add(card_image);
            context.SaveChanges();
            return Ok();
        }
    }
}