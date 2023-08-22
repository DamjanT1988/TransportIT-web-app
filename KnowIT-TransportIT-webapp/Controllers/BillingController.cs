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
    public class BillingController : ControllerBase
    {
        private readonly BillingContext _context;
        private readonly Service _service;

        public BillingController(BillingContext context, Service service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Billing
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillingModel>>> GetBillingModel()
        {
          if (_context.BillingModel == null)
          {
              return NotFound();
          }
            return await _context.BillingModel.ToListAsync();
        }

        // GET: api/Billing/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillingModel>> GetBillingModel(int id)
        {
          if (_context.BillingModel == null)
          {
              return NotFound();
          }
            var billingModel = await _context.BillingModel.FindAsync(id);

            if (billingModel == null)
            {
                return NotFound();
            }

            return billingModel;
        }

        // PUT: api/Billing/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillingModel(int id, BillingModel billingModel)
        {
            if (id != billingModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(billingModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingModelExists(id))
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

        // POST: api/Billing
        [HttpPost]
        public async Task<ActionResult<BillingModel>> PostBillingModel(BillingModel billingModel)
        {
          if (_context.BillingModel == null)
          {
              return Problem("Entity set 'BillingContext.BillingModel'  is null.");
          }

            //set purchase date and ticket status
            billingModel.PurchaseDate = DateTime.Now;
            billingModel.Status = true;

            var tickets = _service.GetTickets();

            // Check if Order string exists and if TicketCost is null or not set
            if (!string.IsNullOrWhiteSpace(billingModel.Order) && !billingModel.TicketCost.HasValue)
            {
                // Try to parse the Order string to get the ticket number
                if (int.TryParse(billingModel.Order, out int orderNumber))
                {
                    var matchingTicket = tickets.FirstOrDefault(t => t.TicketNumber == orderNumber);
                    if (matchingTicket != null)
                    {
                        billingModel.TicketCost = matchingTicket.Price.Value;
                        billingModel.CheckTransport = null;
                        billingModel.Status = true;

                        // Update the Order string with the Category information
                        billingModel.Order += $" - {matchingTicket.Category} on {matchingTicket.WeekDay}";
                    }
                }
            }

            _context.BillingModel.Add(billingModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillingModel", new { id = billingModel.Id }, billingModel);
        }

        // DELETE: api/Billing/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillingModel(int id)
        {
            if (_context.BillingModel == null)
            {
                return NotFound();
            }
            var billingModel = await _context.BillingModel.FindAsync(id);
            if (billingModel == null)
            {
                return NotFound();
            }

            _context.BillingModel.Remove(billingModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillingModelExists(int id)
        {
            return (_context.BillingModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
