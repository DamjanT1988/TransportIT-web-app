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

    public class AdminTicketsController : Controller
    {
        private readonly TicketsContext _context;

        public AdminTicketsController(TicketsContext context)
        {
            _context = context;
        }

        // GET: AdminTickets
        public async Task<IActionResult> Index()
        {
            return _context.TicketsClass != null ?
                        View(await _context.TicketsClass.ToListAsync()) :
                        Problem("Entity set 'TicketsContext.TicketsClass'  is null.");
        }

        // GET: AdminTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TicketsClass == null)
            {
                return NotFound();
            }

            var ticketsModel = await _context.TicketsClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketsModel == null)
            {
                return NotFound();
            }

            return View(ticketsModel);
        }

        // GET: AdminTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TripTitle,TicketNumber,TicketDescription,Price,TicketAvailable,Category,WeekDay, ImagePath")] TicketsModel ticketsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketsModel);
        }

        // GET: AdminTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TicketsClass == null)
            {
                return NotFound();
            }

            var ticketsModel = await _context.TicketsClass.FindAsync(id);
            if (ticketsModel == null)
            {
                return NotFound();
            }
            return View(ticketsModel);
        }

        // POST: AdminTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TripTitle,TicketNumber,TicketDescription,Price,TicketAvailable,Category,WeekDay")] TicketsModel ticketsModel)
        {
            /*
            if (id != ticketsModel.Id)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsModelExists(ticketsModel.Id))
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
            return View(ticketsModel);
        }

        // GET: AdminTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TicketsClass == null)
            {
                return NotFound();
            }

            var ticketsModel = await _context.TicketsClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketsModel == null)
            {
                return NotFound();
            }

            return View(ticketsModel);
        }

        // POST: AdminTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TicketsClass == null)
            {
                return Problem("Entity set 'TicketsContext.TicketsClass'  is null.");
            }
            var ticketsModel = await _context.TicketsClass.FindAsync(id);
            if (ticketsModel != null)
            {
                _context.TicketsClass.Remove(ticketsModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsModelExists(int id)
        {
            return (_context.TicketsClass?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
