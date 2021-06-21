using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;

namespace Presentation.Controllers
{
    public class PortionsController : Controller
    {
        private readonly AppDbContext _context;

        public PortionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Portions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Portions.Include(p => p.Route).Include(p => p.State);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Portions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portion = await _context.Portions
                .Include(p => p.Route)
                .Include(p => p.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portion == null)
            {
                return NotFound();
            }

            return View(portion);
        }

        // GET: Portions/Create
        public IActionResult Create()
        {
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Label");
            return View();
        }

        // POST: Portions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartPortion,EndPortion,RouteId,StateId")] Portion portion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", portion.RouteId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Label", portion.StateId);
            return View(portion);
        }

        // GET: Portions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portion = await _context.Portions.FindAsync(id);
            if (portion == null)
            {
                return NotFound();
            }
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", portion.RouteId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Label", portion.StateId);
            return View(portion);
        }

        // POST: Portions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartPortion,EndPortion,RouteId,StateId")] Portion portion)
        {
            if (id != portion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortionExists(portion.Id))
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
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", portion.RouteId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Label", portion.StateId);
            return View(portion);
        }

        // GET: Portions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portion = await _context.Portions
                .Include(p => p.Route)
                .Include(p => p.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portion == null)
            {
                return NotFound();
            }

            return View(portion);
        }

        // POST: Portions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portion = await _context.Portions.FindAsync(id);
            _context.Portions.Remove(portion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortionExists(int id)
        {
            return _context.Portions.Any(e => e.Id == id);
        }
    }
}
