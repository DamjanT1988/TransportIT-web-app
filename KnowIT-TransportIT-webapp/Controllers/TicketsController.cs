using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KnowIT_TransportIT_webapp.Data;
using KnowIT_TransportIT_webapp.Models;

namespace KnowIT_TransportIT_webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsContext _context;

        public TicketsController(TicketsContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketsClass>>> GetTicketsClass()
        {
          if (_context.TicketsClass == null)
          {
              return NotFound();
          }
            return await _context.TicketsClass.ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketsClass>> GetTicketsClass(int id)
        {
          if (_context.TicketsClass == null)
          {
              return NotFound();
          }
            var ticketsClass = await _context.TicketsClass.FindAsync(id);

            if (ticketsClass == null)
            {
                return NotFound();
            }

            return ticketsClass;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketsClass(int id, TicketsClass ticketsClass)
        {
            if (id != ticketsClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticketsClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketsClassExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketsClass>> PostTicketsClass(TicketsClass ticketsClass)
        {
          if (_context.TicketsClass == null)
          {
              return Problem("Entity set 'TicketsContext.TicketsClass'  is null.");
          }
            _context.TicketsClass.Add(ticketsClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketsClass", new { id = ticketsClass.Id }, ticketsClass);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketsClass(int id)
        {
            if (_context.TicketsClass == null)
            {
                return NotFound();
            }
            var ticketsClass = await _context.TicketsClass.FindAsync(id);
            if (ticketsClass == null)
            {
                return NotFound();
            }

            _context.TicketsClass.Remove(ticketsClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketsClassExists(int id)
        {
            return (_context.TicketsClass?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
