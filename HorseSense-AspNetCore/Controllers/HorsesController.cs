using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HorseSense_AspNetCore.Models;

namespace HorseSense_AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorsesController : ControllerBase
    {
        private readonly HorseSenseContext _context;

        public HorsesController(HorseSenseContext context)
        {
            _context = context;
        }

        // GET: api/Horses
        [HttpGet]
        public IEnumerable<Horse> GetHorses()
        {
            return _context.Horses;
        }

        // GET: api/Horses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHorse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var horse = await _context.Horses.FindAsync(id);

            if (horse == null)
            {
                return NotFound();
            }

            return Ok(horse);
        }

        // PUT: api/Horses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorse([FromRoute] int id, [FromBody] Horse horse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != horse.HorseId)
            {
                return BadRequest();
            }

            _context.Entry(horse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Horses
        [HttpPost]
        public async Task<IActionResult> PostHorse([FromBody] Horse horse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Horses.Add(horse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorse", new { id = horse.HorseId }, horse);
        }

        // DELETE: api/Horses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var horse = await _context.Horses.FindAsync(id);
            if (horse == null)
            {
                return NotFound();
            }

            _context.Horses.Remove(horse);
            await _context.SaveChangesAsync();

            return Ok(horse);
        }

        private bool HorseExists(int id)
        {
            return _context.Horses.Any(e => e.HorseId == id);
        }
    }
}