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

        public AdminBillingsController(BillingContext context)
        {
            _context = context;
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

        // POST: AdminBillings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TicketCost,Order,Email,Telephone,CustomerName,PassangerNo,CheckTransport,Status,InternalNote, StartDate, EndDate, StartTime, EndTime,  PurchaseDate")] BillingModel billingModel)
        {

            //check ticket cost is >200
            // Check if the TicketCost is more than 200
            if (billingModel.TicketCost > 200)
            {
                billingModel.TicketCost = 200;
            }


            // Set PurchaseDate to the current date
            billingModel.PurchaseDate = DateTime.Now;





            //check model state, then save, return to index page
            if (ModelState.IsValid)
            {
                _context.Add(billingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //return view
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

    }
}
