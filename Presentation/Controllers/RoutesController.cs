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
    public class RoutesController : Controller
    {
        private readonly AppDbContext _context;

        public RoutesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Routes.Include(r => r.EndCityNavigation).Include(r => r.StartCityNavigation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.EndCityNavigation)
                .Include(r => r.StartCityNavigation)
                .Include(r => r.Portions)
                .ThenInclude(p => p.State)
                .FirstOrDefaultAsync(m => m.Id == id);

            Models.Route.RouteDetailsViewModel viewModel = new Models.Route.RouteDetailsViewModel
            {
                Route = route,
                Portions = route.Portions.ToList(),
                TotalDuration = route.Portions.Sum(p => p.GetDureeReparation()),
                TotalPrice = route.Portions.Sum(p => p.GetPrixReparation()),
            };

            if (route == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["EndCity"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["StartCity"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartCity,EndCity,Kmlength,Etat")] Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EndCity"] = new SelectList(_context.Cities, "Id", "Name", route.EndCity);
            ViewData["StartCity"] = new SelectList(_context.Cities, "Id", "Name", route.StartCity);
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            ViewData["EndCity"] = new SelectList(_context.Cities, "Id", "Name", route.EndCity);
            ViewData["StartCity"] = new SelectList(_context.Cities, "Id", "Name", route.StartCity);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartCity,EndCity,Kmlength,Etat")] Route route)
        {
            if (id != route.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Id))
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
            ViewData["EndCity"] = new SelectList(_context.Cities, "Id", "Name", route.EndCity);
            ViewData["StartCity"] = new SelectList(_context.Cities, "Id", "Name", route.StartCity);
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.EndCityNavigation)
                .Include(r => r.StartCityNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Validate(int id)
        {
            Route route = await _context.Routes
                .Include(r => r.EndCityNavigation)
                .Include(r => r.StartCityNavigation)
                .Include(r => r.Portions)
                .ThenInclude(p => p.State)
                .FirstOrDefaultAsync(m => m.Id == id);

            Models.Route.RouteDetailsViewModel viewModel = new Models.Route.RouteDetailsViewModel
            {
                Route = route,
                Portions = route.Portions.ToList(),
                TotalDuration = route.Portions.Sum(p => p.GetDureeReparation()),
                TotalPrice = route.Portions.Sum(p => p.GetPrixReparation()),
            };

            if (route.isValid())
            {
                route.Etat = "Valide";
                _context.Update(route);
                await _context.SaveChangesAsync();
                return View("details", viewModel);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Les portions de la route ne sont pas" +
                    " cohérents pour la valider.");
                return View("details", viewModel);
            }
        }
    }
}
