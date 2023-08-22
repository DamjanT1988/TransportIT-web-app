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

            // Default PurchaseDate to the current system date if not set
            if (billingModel.PurchaseDate == null)
            {
                billingModel.PurchaseDate = DateTime.Now;
            }

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


            // Retrieve all FreeDays from the service and FreeDay db
            var freeDays = _service.GetFreeDays() ?? new List<FreeDayClass>();

            // Determine if the PurchaseDate is within a FreeDay range
            foreach (var freeDay in freeDays)
            {
                if (billingModel.PurchaseDate >= freeDay.StartDateFreeDay && billingModel.PurchaseDate <= freeDay.EndDateFreeDay)
                {
                    billingModel.TicketCost = 0;

                    // Mark the ticket as free with the respective date
                    billingModel.Order = $"FREE ON DAY {billingModel.PurchaseDate.Value.ToShortDateString()}";

                    break; // Terminate the loop if a matching FreeDay is found
                }
            }


            // Checking if the billing model overlaps two different days
            // This situation arises when a fare or journey begins on one day and ends on another
            if (billingModel.StartDate != billingModel.EndDate)
            {
                // Fetch all available tickets using the provided service method
                //var tickets = _service.GetTickets();

                // Check if the journey's StartDate is within the range of any FreeDay
                bool isStartDateFree = freeDays.Any(fd => billingModel.StartDate >= fd.StartDateFreeDay && billingModel.StartDate <= fd.EndDateFreeDay);
                // Similarly, check if the journey's EndDate falls within a FreeDay
                bool isEndDateFree = freeDays.Any(fd => billingModel.EndDate >= fd.StartDateFreeDay && billingModel.EndDate <= fd.EndDateFreeDay);

                double costDay1 = 0; // Initialize the total cost for StartDate

                // If the StartDate isn't a FreeDay, we compute the costs associated with that day.
                if (!isStartDateFree)
                {
                    // Retrieve all tickets purchased by the passenger on the StartDate.
                    var ticketsForDay1 = _context.BillingModel
                                                 .Where(b => b.PassangerNo == billingModel.PassangerNo && b.PurchaseDate == billingModel.StartDate)
                                                 .ToList();

                    // Loop through each ticket to calculate the total cost.
                    foreach (var ticket in ticketsForDay1)
                    {
                        int orderNumber;
                        // Try parsing the ticket's Order string to extract the ticket number.
                        if (int.TryParse(ticket.Order, out orderNumber))
                        {
                            // Find the corresponding ticket details from the fetched ticket list.
                            var matchingTicket = tickets.FirstOrDefault(t => t.TicketNumber == orderNumber);
                            if (matchingTicket != null)
                            {
                                // Add the ticket's price to the day's total cost.
                                costDay1 += matchingTicket.Price.Value;
                            }
                        }
                    }
                }

                double costDay2 = 0; // Initialize the total cost for EndDate

                // If the EndDate isn't a FreeDay, we compute the costs associated with that day.
                if (!isEndDateFree)
                {
                    // Retrieve all tickets purchased by the passenger on the EndDate.
                    var ticketsForDay2 = _context.BillingModel
                                                 .Where(b => b.PassangerNo == billingModel.PassangerNo && b.PurchaseDate == billingModel.EndDate)
                                                 .ToList();

                    // Loop through each ticket to calculate the total cost.
                    foreach (var ticket in ticketsForDay2)
                    {
                        int orderNumber;
                        // Try parsing the ticket's Order string to extract the ticket number.
                        if (int.TryParse(ticket.Order, out orderNumber))
                        {
                            // Find the corresponding ticket details from the fetched ticket list.
                            var matchingTicket = tickets.FirstOrDefault(t => t.TicketNumber == orderNumber);
                            if (matchingTicket != null)
                            {
                                // Add the ticket's price to the day's total cost.
                                costDay2 += matchingTicket.Price.Value;
                            }
                        }
                    }
                }

                // If both the StartDate and EndDate are FreeDays, set the fare to be free of charge
                if (isStartDateFree && isEndDateFree)
                {
                    billingModel.TicketCost = 0;
                    billingModel.Order = $"FREE ON BOTH DAYS {billingModel.StartDate.Value.ToShortDateString()} and {billingModel.EndDate.Value.ToShortDateString()}";
                }
                else
                {
                    // If only one of the days is a FreeDay, then we set the PurchaseDate to the day with the higher ticket cost.
                    billingModel.PurchaseDate = (costDay2 > costDay1) ? billingModel.EndDate : billingModel.StartDate;
                }
            }

            // Calculate the total amount spent by the passenger for the day
            var totalSpentToday = _context.BillingModel
                                          .Where(b => b.PassangerNo == billingModel.PassangerNo &&
                                                      b.PurchaseDate.Value.Date == billingModel.PurchaseDate.Value.Date)
                                          .Sum(b => b.TicketCost);

            // Adjust the TicketCost if the combined total exceeds 200
            if (billingModel.TicketCost >= 200)
            {
                billingModel.TicketCost = 200;
                billingModel.Order = $"FREE REST OF DAY - SPENT 200 SEK. DAY {billingModel.PurchaseDate.Value.ToShortDateString()}";
            }
            else if (totalSpentToday + billingModel.TicketCost >= 200)
            {
                billingModel.TicketCost = 200 - totalSpentToday;
                billingModel.Order = $"FREE REST OF DAY - SPENT MORE THAN 200 SEK TODAY ON {billingModel.PurchaseDate.Value.ToShortDateString()}";
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
