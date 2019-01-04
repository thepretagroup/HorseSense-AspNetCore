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
    public class RaceDaysController : ControllerBase
    {
        private readonly HorseSenseContext _context;

        public RaceDaysController(HorseSenseContext context)
        {
            _context = context;
        }

        // GET: api/RaceDays
        [HttpGet]
        public IEnumerable<RaceDay> GetRaceDays()
        {
            return _context.RaceDays;
        }

        // GET: api/RaceDays/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRaceDay([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var raceDay = await _context.RaceDays.FindAsync(id);

            if (raceDay == null)
            {
                return NotFound();
            }

            return Ok(raceDay);
        }

        // PUT: api/RaceDays/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRaceDay([FromRoute] int id, [FromBody] RaceDay raceDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != raceDay.RaceDayId)
            {
                return BadRequest();
            }

            _context.Entry(raceDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceDayExists(id))
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

        // POST: api/RaceDays
        [HttpPost]
        public async Task<IActionResult> PostRaceDay([FromBody] RaceDay raceDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RaceDays.Add(raceDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRaceDay", new { id = raceDay.RaceDayId }, raceDay);
        }

        // DELETE: api/RaceDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaceDay([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var raceDay = await _context.RaceDays.FindAsync(id);
            if (raceDay == null)
            {
                return NotFound();
            }

            _context.RaceDays.Remove(raceDay);
            await _context.SaveChangesAsync();

            return Ok(raceDay);
        }

        private bool RaceDayExists(int id)
        {
            return _context.RaceDays.Any(e => e.RaceDayId == id);
        }
    }
}