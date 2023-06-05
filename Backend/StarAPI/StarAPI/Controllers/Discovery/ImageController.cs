using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using System;
using StarAPI.Context;

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
        public IEnumerable<Image> GetAllImages()
        {
            return context.Image.ToList();
        }

        // GET api/<DeckController>/5
        [HttpPost("{image_string}")]
        public ActionResult AddImage([FromBody] Image card_image)
        {
            context.Image.Add(card_image);
            context.SaveChanges();
            return Ok();
        }

        [HttpPost("AddImages")]
        public ActionResult AddImages([FromBody] List<Image> card_images)
        {
            foreach(var image in card_images)
            {
                context.Image.Add(image);
                
            }
            context.SaveChanges();

            return Ok();
        }
    }
}