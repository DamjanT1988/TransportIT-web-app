using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowIT_TransportIT_webapp.Data;
using KnowIT_TransportIT_webapp.Models;

namespace KnowIT_TransportIT_webapp.Controllers
{
    public class AdminFreeDayController : Controller
    {
        private readonly FreeDayContext _context;

        public AdminFreeDayController(FreeDayContext context)
        {
            _context = context;
        }

        // GET: AdminFreeDay
        public async Task<IActionResult> Index()
        {
            return _context.FreeDayClass != null ?
                        View(await _context.FreeDayClass.ToListAsync()) :
                        Problem("Entity set 'FreeDayContext.FreeDayClass'  is null.");
        }

        // GET: AdminFreeDay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FreeDayClass == null)
            {
                return NotFound();
            }

            var freeDayClass = await _context.FreeDayClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (freeDayClass == null)
            {
                return NotFound();
            }

            return View(freeDayClass);
        }

        // GET: AdminFreeDay/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminFreeDay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FreeDayReason,StatusFreeDay,StartDateFreeDay,EndDateFreeDay")] FreeDayClass freeDayClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(freeDayClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(freeDayClass);
        }

        // GET: AdminFreeDay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FreeDayClass == null)
            {
                return NotFound();
            }

            var freeDayClass = await _context.FreeDayClass.FindAsync(id);
            if (freeDayClass == null)
            {
                return NotFound();
            }
            return View(freeDayClass);
        }

        // POST: AdminFreeDay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FreeDayReason,StatusFreeDay,StartDateFreeDay,EndDateFreeDay")] FreeDayClass freeDayClass)
        {
            if (id != freeDayClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(freeDayClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FreeDayClassExists(freeDayClass.Id))
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
            return View(freeDayClass);
        }

        // GET: AdminFreeDay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FreeDayClass == null)
            {
                return NotFound();
            }

            var freeDayClass = await _context.FreeDayClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (freeDayClass == null)
            {
                return NotFound();
            }

            return View(freeDayClass);
        }

        // POST: AdminFreeDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FreeDayClass == null)
            {
                return Problem("Entity set 'FreeDayContext.FreeDayClass'  is null.");
            }
            var freeDayClass = await _context.FreeDayClass.FindAsync(id);
            if (freeDayClass != null)
            {
                _context.FreeDayClass.Remove(freeDayClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FreeDayClassExists(int id)
        {
            return (_context.FreeDayClass?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
