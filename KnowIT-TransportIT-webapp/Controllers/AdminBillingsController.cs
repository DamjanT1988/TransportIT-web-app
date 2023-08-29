using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowIT_TransportIT_webapp.Data;
using KnowIT_TransportIT_webapp.Models;
using Microsoft.AspNetCore.Authorization;

namespace KnowIT_TransportIT_webapp.Controllers
{
    //authorize the whole class
    [Authorize]

    public class AdminBillingsController : Controller
    {
        private readonly BillingContext _context;
        private readonly Service _service;

        public AdminBillingsController(BillingContext context, Service service)
        {
            _context = context;
            _service = service;
        }

        // GET: AdminBillings
        public async Task<IActionResult> Index()
        {
            return _context.BillingModel != null ?
                        View(await _context.BillingModel.ToListAsync()) :
                        Problem("Entity set 'BillingContext.BillingModel'  is null.");
        }

        // GET: AdminBillings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BillingModel == null)
            {
                return NotFound();
            }

            var billingModel = await _context.BillingModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billingModel == null)
            {
                return NotFound();
            }

            return View(billingModel);
        }

        // GET: AdminBillings/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TicketCost,Order,PassangerNo,CheckTransport,Status,StartDate, EndDate, StartTime, EndTime,  PurchaseDate")] BillingModel billingModel)
        {
            // Default PurchaseDate to the current system date if not set
            if (billingModel.PurchaseDate == null)
            {
                billingModel.PurchaseDate = DateTime.Now;
            }

            // Get tickets list from db
            var tickets = _service.GetTickets();

            // Check if Order string exists and if TicketCost is null or not set
            if (!string.IsNullOrWhiteSpace(billingModel.Order) && !billingModel.TicketCost.HasValue)
            {
                // Try to parse the Order string to get the ticket number
                if (int.TryParse(billingModel.Order, out int orderNumber))
                {
                    // Match order number with ticket number
                    var matchingTicket = tickets.FirstOrDefault(t => t.TicketNumber == orderNumber);
                    
                    // If there is a match, set data
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


            // Retrieve all FreeDays from the service and FreeDay db, make new list
            var freeDays = _service.GetFreeDays() ?? new List<FreeDayClass>();

            // Determine if the PurchaseDate is within a FreeDay range
            foreach (var freeDay in freeDays)
            {
                // Check FreeDays
                if (billingModel.PurchaseDate >= freeDay.StartDateFreeDay && billingModel.PurchaseDate <= freeDay.EndDateFreeDay)
                {
                    billingModel.TicketCost = 0;

                    // Mark the ticket as free with the respective date
                    billingModel.Order = $"FREE ON DAY {billingModel.PurchaseDate.Value.ToShortDateString()}";

                    break; // Terminate the loop if a matching FreeDay is found
                }
            }


            // Checking if the billing model overlaps two different days
            // This situation arises when a fare begins on one day and ends on another
            if (billingModel.StartDate != billingModel.EndDate || billingModel.StartDate == billingModel.EndDate && billingModel.EndDate != null) //EDIT - ADDED ||
            {
                // Fetch all available tickets using the provided service method
                //var tickets = _service.GetTickets();

                // Check if the journey's StartDate is within the range of any FreeDay
                bool isStartDateFree = freeDays.Any(fd => billingModel.StartDate >= fd.StartDateFreeDay && billingModel.StartDate <= fd.EndDateFreeDay);
                // Similarly, check if the journey's EndDate falls within a FreeDay
                bool isEndDateFree = freeDays.Any(fd => billingModel.EndDate >= fd.StartDateFreeDay && billingModel.EndDate <= fd.EndDateFreeDay);

                double costDay1 = 0; // Initialize the total cost for StartDate


                // If the StartDate isn't a FreeDay, compute the costs associated with that day.
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

                // If the EndDate isn't a FreeDay, compute the costs associated with that day.
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



                // If both the StartDate and EndDate are the same day, set to 0 and update Order information
                if (isEndDateFree && isStartDateFree && billingModel.StartDate == billingModel.EndDate) //ADDED
                {
                    billingModel.TicketCost = 0;
                    billingModel.Order = $"FREE ON DAY {billingModel.StartDate.Value.ToShortDateString()}";
                }
                // If both the StartDate and EndDate are FreeDays, set the fare to be free of charge
                else if (isStartDateFree && isEndDateFree)
                {
                    billingModel.TicketCost = 0;
                    billingModel.Order = $"FREE ON BOTH DAYS {billingModel.StartDate.Value.ToShortDateString()} and {billingModel.EndDate.Value.ToShortDateString()}";
                }
                else
                {
                    // If only one of the days is a FreeDay, then set the PurchaseDate to the day with the higher ticket cost.
                    billingModel.PurchaseDate = (costDay2 > costDay1) ? billingModel.EndDate : billingModel.StartDate; // Billing value date instead?
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
            // Check total amount a day
            else if (totalSpentToday + billingModel.TicketCost >= 200)
            {
                billingModel.TicketCost = 200 - totalSpentToday;
                billingModel.Order = $"FREE REST OF DAY - SPENT MORE THAN 200 SEK TODAY ON {billingModel.PurchaseDate.Value.ToShortDateString()}";
            }

            // Validate model state, save changes, and redirect to the index page.
            if (ModelState.IsValid)
            {
                _context.Add(billingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Return the current view if the model is invalid.
            return View(billingModel);
        }

        // GET: AdminBillings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BillingModel == null)
            {
                return NotFound();
            }

            var billingModel = await _context.BillingModel.FindAsync(id);
            if (billingModel == null)
            {
                return NotFound();
            }
            return View(billingModel);
        }

        // POST: AdminBillings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketCost,Order,PassangerNo,CheckTransport,Status, StartDate, EndDate, StartTime, EndTime, PurchaseDate")] BillingModel billingModel)
        {
            if (id != billingModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingModelExists(billingModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(billingModel);
        }

        // GET: AdminBillings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BillingModel == null)
            {
                return NotFound();
            }

            var billingModel = await _context.BillingModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billingModel == null)
            {
                return NotFound();
            }

            return View(billingModel);
        }

        // POST: AdminBillings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BillingModel == null)
            {
                return Problem("Entity set 'BillingContext.BillingModel'  is null.");
            }
            var billingModel = await _context.BillingModel.FindAsync(id);
            if (billingModel != null)
            {
                _context.BillingModel.Remove(billingModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillingModelExists(int id)
        {
            return (_context.BillingModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        //OWN METHODS
        // Action for checking in the passenger
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckIn(int id)
        {
            // Find the billing model by ID.
            var billing = await _context.BillingModel.FindAsync(id);

            // If the billing model with the given ID doesn't exist, return a NotFound result.
            if (billing == null)
            {
                return NotFound();
            }

            // Update the transport and status flags for the billing model.
            billing.CheckTransport = true;
            billing.Status = true;

            // Update the billing model in the database context.
            _context.Update(billing);
            // Save the changes to the database.
            await _context.SaveChangesAsync();

            // Redirect to the Index action method.
            return RedirectToAction(nameof(Index));
        }

        // Action for checking out the passenger
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(int id)
        {
            // Find the billing model by ID.
            var billing = await _context.BillingModel.FindAsync(id);

            // If the billing model with the given ID doesn't exist, return a NotFound result.
            if (billing == null)
            {
                return NotFound();
            }

            // Update the transport and status flags for the billing model.
            billing.CheckTransport = false;
            billing.Status = false;

            // Update the billing model in the database context.
            _context.Update(billing);
            // Save the changes to the database.
            await _context.SaveChangesAsync();

            // Redirect to the Index action method.
            return RedirectToAction(nameof(Index));
        }


    }
}
