using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Authorize]
    public class ReparePortionsController : Controller
    {
        private readonly AppDbContext _context;

        public ReparePortionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReparePortions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ReparePortions.Include(r => r.Portion);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ReparePortions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparePortion = await _context.ReparePortions
                .Include(r => r.Portion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reparePortion == null)
            {
                return NotFound();
            }

            return View(reparePortion);
        }

        // GET: ReparePortions/Create
        public IActionResult Create()
        {
            ViewData["PortionId"] = new SelectList(_context.Portions, "Id", "Name");
            return View();
        }

        // POST: ReparePortions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateRep,PortionId")] ReparePortion model)
        {
            Portion portion = await _context.Portions
                .Include(p => p.State)
                .FirstOrDefaultAsync(p => p.Id == model.PortionId);

            if (ModelState.IsValid)
            {
                if (portion.State.Label != "Tres Bien")
                {
                    decimal durre = portion.GetDureeReparation();
                    decimal prix = portion.GetPrixReparation();

                    decimal totalBudget = Budget.GetTotalBudget(await _context.Budgets.ToListAsync(), await _context.ReparePortions.ToListAsync());

                    if (totalBudget < prix)
                    {
                        ModelState.AddModelError(string.Empty, "Budget insuffisant");
                        ViewData["PortionId"] = new SelectList(_context.Portions, "Id", "Name", model.PortionId);
                        return View(model);
                    }

                    ReparePortion newRepare = new ReparePortion
                    {
                        PortionId = model.PortionId,
                        DateRep = model.DateRep,
                        DureeReparation = durre,
                        PrixReparation = prix
                    };
                    State state = await _context.States.FirstOrDefaultAsync(s => s.Label == "Tres Bien");

                    _context.Add(newRepare);
                    portion.StateId = state.Id;
                    _context.Update(portion);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Une portion de Bonne état n'a pas besoins d'être réparée");
                ViewData["PortionId"] = new SelectList(_context.Portions, "Id", "Name", model.PortionId);
                return View(model);
            }
            ViewData["PortionId"] = new SelectList(_context.Portions, "Id", "Name", model.PortionId);
            return View(model);
        }

        // GET: ReparePortions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparePortion = await _context.ReparePortions.FindAsync(id);
            if (reparePortion == null)
            {
                return NotFound();
            }
            ViewData["PortionId"] = new SelectList(_context.Portions, "Id", "Name", reparePortion.PortionId);
            return View(reparePortion);
        }

        // POST: ReparePortions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateRep,PortionId,DureeReparation,PrixReparation")] ReparePortion reparePortion)
        {
            if (id != reparePortion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reparePortion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReparePortionExists(reparePortion.Id))
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
            ViewData["PortionId"] = new SelectList(_context.Portions, "Id", "Name", reparePortion.PortionId);
            return View(reparePortion);
        }

        // GET: ReparePortions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reparePortion = await _context.ReparePortions
                .Include(r => r.Portion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reparePortion == null)
            {
                return NotFound();
            }

            return View(reparePortion);
        }

        // POST: ReparePortions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reparePortion = await _context.ReparePortions.FindAsync(id);
            _context.ReparePortions.Remove(reparePortion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReparePortionExists(int id)
        {
            return _context.ReparePortions.Any(e => e.Id == id);
        }
    }
}
