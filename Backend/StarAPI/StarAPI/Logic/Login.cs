﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Logic.Utils;
using StarAPI.Models;
using StarAPI.Context;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Logic.Login
{
    [Route("[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly StarDeckContext _context;
        private Encrypt encrypt;
        public Login(StarDeckContext context)
        {
            _context = context;
            encrypt = new Encrypt();
        }

        // GET: api/<Login>
        [HttpGet("{email}/{password}")]
        public ActionResult Get(string email, string password)
        {

            Player player = _context.Player.FirstOrDefault(p => p.email == email);

            if (player != null && encrypt.Sha256(password) == player.pHash)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}