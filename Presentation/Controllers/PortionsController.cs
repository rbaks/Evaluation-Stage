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
            var appDbContext = _context.Portions.Include(p => p.PreviousNavigation).Include(p => p.Route).Include(p => p.State);
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
                .Include(p => p.PreviousNavigation)
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
            ViewData["Previous"] = new SelectList(_context.Portions, "Id", "Name");
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Label");
            return View();
        }

        // POST: Portions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartPortion,EndPortion,RouteId,StateId")] Portion portion)
        {
            Route route = await _context.Routes.FindAsync(portion.RouteId);
            if (ModelState.IsValid)
            {
                if (route.Etat == "Modifiable")
                {
                    portion.Kmlength = (portion.EndPortion - portion.StartPortion);
                    _context.Add(portion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La route correspondante doite être à l'etat modifiable " +
                        "pour pouvoir y ajouter une portion");
                    ViewData["Previous"] = new SelectList(_context.Portions, "Id", "Id", portion.Previous);
                    ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", portion.RouteId);
                    ViewData["StateId"] = new SelectList(_context.States, "Id", "Label", portion.StateId);
                    return View(portion);
                }

            }
            ViewData["Previous"] = new SelectList(_context.Portions, "Id", "Id", portion.Previous);
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
            ViewData["Previous"] = new SelectList(_context.Portions, "Id", "Id", portion.Previous);
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", portion.RouteId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Label", portion.StateId);
            return View(portion);
        }

        // POST: Portions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartPortion,EndPortion,RouteId,StateId")] Portion portion)
        {
            if (id != portion.Id)
            {
                return NotFound();
            }

            Route route = await _context.Routes.FindAsync(portion.RouteId);

            if (ModelState.IsValid)
            {
                try
                {
                    if (route.Etat == "Modifiable")
                    {
                        portion.Kmlength = (portion.EndPortion - portion.StartPortion);
                        _context.Update(portion);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "La route correspondante doite être à l'etat modifiable " +
                            "pour pouvoir modifier cette portion");
                        ViewData["Previous"] = new SelectList(_context.Portions, "Id", "Id", portion.Previous);
                        ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", portion.RouteId);
                        ViewData["StateId"] = new SelectList(_context.States, "Id", "Label", portion.StateId);
                        return View(portion);
                    }
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
            ViewData["Previous"] = new SelectList(_context.Portions, "Id", "Id", portion.Previous);
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
                .Include(p => p.PreviousNavigation)
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
