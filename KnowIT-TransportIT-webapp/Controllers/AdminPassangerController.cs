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
    public class AdminPassangerController : Controller
    {
        private readonly PassangerContext _context;

        public AdminPassangerController(PassangerContext context)
        {
            _context = context;
        }

        // GET: AdminPassanger
        public async Task<IActionResult> Index()
        {
            return _context.PassangerModel != null ?
                        View(await _context.PassangerModel.ToListAsync()) :
                        Problem("Entity set 'PassangerContext.PassangerModel'  is null.");
        }

        // GET: AdminPassanger/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PassangerModel == null)
            {
                return NotFound();
            }

            var passangerModel = await _context.PassangerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passangerModel == null)
            {
                return NotFound();
            }

            return View(passangerModel);
        }

        // GET: AdminPassanger/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPassanger/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PassangerName,PassangerSocNo,CreationAccount")] PassangerModel passangerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passangerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passangerModel);
        }

        // GET: AdminPassanger/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PassangerModel == null)
            {
                return NotFound();
            }

            var passangerModel = await _context.PassangerModel.FindAsync(id);
            if (passangerModel == null)
            {
                return NotFound();
            }
            return View(passangerModel);
        }

        // POST: AdminPassanger/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PassangerName,PassangerSocNo,CreationAccount")] PassangerModel passangerModel)
        {
            if (id != passangerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passangerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassangerModelExists(passangerModel.Id))
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
            return View(passangerModel);
        }

        // GET: AdminPassanger/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PassangerModel == null)
            {
                return NotFound();
            }

            var passangerModel = await _context.PassangerModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passangerModel == null)
            {
                return NotFound();
            }

            return View(passangerModel);
        }

        // POST: AdminPassanger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PassangerModel == null)
            {
                return Problem("Entity set 'PassangerContext.PassangerModel'  is null.");
            }
            var passangerModel = await _context.PassangerModel.FindAsync(id);
            if (passangerModel != null)
            {
                _context.PassangerModel.Remove(passangerModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassangerModelExists(int id)
        {
            return (_context.PassangerModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
