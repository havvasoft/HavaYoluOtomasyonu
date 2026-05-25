using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HavaYoluOtomasyonu.Models;
using Route = HavaYoluOtomasyonu.Models.Route;

namespace HavaYoluOtomasyonu.Controllers
{
    public class RoutesController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public RoutesController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index()
        {
            var havayoluOtomasyonDbContext = _context.Routes.Include(r => r.ArrivalAirport).Include(r => r.DepartureAirport);
            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.ArrivalAirport)
                .Include(r => r.DepartureAirport)
                .FirstOrDefaultAsync(m => m.RoutesId == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["ArrivalAirportId"] = new SelectList(_context.Airports, "AirportId", "AirportId");
            ViewData["DepartureAirportId"] = new SelectList(_context.Airports, "AirportId", "AirportId");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoutesId,DepartureAirportId,ArrivalAirportId,Distance,EstimatedDuration")] Route route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArrivalAirportId"] = new SelectList(_context.Airports, "AirportId", "AirportId", route.ArrivalAirportId);
            ViewData["DepartureAirportId"] = new SelectList(_context.Airports, "AirportId", "AirportId", route.DepartureAirportId);
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
            ViewData["ArrivalAirportId"] = new SelectList(_context.Airports, "AirportId", "AirportId", route.ArrivalAirportId);
            ViewData["DepartureAirportId"] = new SelectList(_context.Airports, "AirportId", "AirportId", route.DepartureAirportId);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoutesId,DepartureAirportId,ArrivalAirportId,Distance,EstimatedDuration")] Route route)
        {
            if (id != route.RoutesId)
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
                    if (!RouteExists(route.RoutesId))
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
            ViewData["ArrivalAirportId"] = new SelectList(_context.Airports, "AirportId", "AirportId", route.ArrivalAirportId);
            ViewData["DepartureAirportId"] = new SelectList(_context.Airports, "AirportId", "AirportId", route.DepartureAirportId);
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
                .Include(r => r.ArrivalAirport)
                .Include(r => r.DepartureAirport)
                .FirstOrDefaultAsync(m => m.RoutesId == id);
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
            if (route != null)
            {
                _context.Routes.Remove(route);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.RoutesId == id);
        }
    }
}
