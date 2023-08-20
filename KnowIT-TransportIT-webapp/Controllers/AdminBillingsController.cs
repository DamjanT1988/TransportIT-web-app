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
        public async Task<IActionResult> Create([Bind("Id,TicketCost,Order,Email,Telephone,CustomerName,PassangerNo,CheckTransport,Status,InternalNote, StartDate, EndDate, StartTime, EndTime,  PurchaseDate")] BillingModel billingModel)
        {
            // Default PurchaseDate to the current system date if not set.
            if (billingModel.PurchaseDate == null)
            {
                billingModel.PurchaseDate = DateTime.Now;
            }

            // Retrieve all FreeDays from the service.
            var freeDays = _service.GetFreeDays() ?? new List<FreeDayClass>();

            // Determine if the PurchaseDate is within a FreeDay range.
            foreach (var freeDay in freeDays)
            {
                if (billingModel.PurchaseDate >= freeDay.StartDateFreeDay && billingModel.PurchaseDate <= freeDay.EndDateFreeDay)
                {
                    billingModel.TicketCost = 0;

                    // Mark the ticket as free with the respective date.
                    billingModel.Order = $"FREE ON DAY {billingModel.PurchaseDate.Value.ToShortDateString()}";

                    break; // Terminate the loop if a matching FreeDay is found.
                }
            }

            // Cap the TicketCost at 200.
            if (billingModel.TicketCost > 200)
            {
                billingModel.TicketCost = 200;
                billingModel.Order = $"FREE REST OF DAY {billingModel.PurchaseDate.Value.ToShortDateString()}";
            }

            // Handle fares that overlap two days.
            if (billingModel.StartDate != billingModel.EndDate)
            {
                var costDay1 = _context.BillingModel
                                       .Where(b => b.PassangerNo == billingModel.PassangerNo && b.PurchaseDate == billingModel.StartDate)
                                       .Sum(b => b.TicketCost);

                var costDay2 = _context.BillingModel
                                       .Where(b => b.PassangerNo == billingModel.PassangerNo && b.PurchaseDate == billingModel.EndDate)
                                       .Sum(b => b.TicketCost);

                // Determine the date with the higher cost.
                billingModel.PurchaseDate = (costDay2 > costDay1) ? billingModel.EndDate : billingModel.StartDate;
            }

            // Calculate the total amount spent by the passenger for the day.
            var totalSpentToday = _context.BillingModel
                                          .Where(b => b.PassangerNo == billingModel.PassangerNo &&
                                                      b.PurchaseDate.Value.Date == billingModel.PurchaseDate.Value.Date)
                                          .Sum(b => b.TicketCost);


            // Adjust the TicketCost if the combined total exceeds 200.
            if (totalSpentToday + billingModel.TicketCost > 200)
            {
                billingModel.TicketCost = 0;
                billingModel.Order = $"SPENT MORE THAN 200 SEK - FREE REST OF DAY {billingModel.PurchaseDate.Value.ToShortDateString()}";
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketCost,Order,Email,Telephone,CustomerName,PassangerNo,CheckTransport,Status,InternalNote, StartDate, EndDate, StartTime, EndTime, PurchaseDate")] BillingModel billingModel)
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
            var billing = await _context.BillingModel.FindAsync(id);

            if (billing == null)
            {
                return NotFound();
            }

            billing.CheckTransport = true;
            billing.Status = true;

            _context.Update(billing);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Action for checking out the passenger
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(int id)
        {
            var billing = await _context.BillingModel.FindAsync(id);

            if (billing == null)
            {
                return NotFound();
            }

            billing.CheckTransport = false;
            billing.Status = false;

            _context.Update(billing);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
