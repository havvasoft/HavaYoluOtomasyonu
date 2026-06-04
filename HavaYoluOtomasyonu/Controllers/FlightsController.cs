using HavaYoluOtomasyonu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HavaYoluOtomasyonu.Controllers
{
    public class FlightsController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public FlightsController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            // Include komutları ile bağlı olan alt tabloları da zorla çekiyoruz.
            var havayoluOtomasyonDbContext = _context.Flights
                .Include(f => f.Aircraft) // Uçak bilgilerini getir
                .Include(f => f.Routes) // Rotaları getir
                    .ThenInclude(r => r.DepartureAirport) // Rotanın içindeki Kalkış Havalimanını getir
                .Include(f => f.Routes) // Rotaları getir
                    .ThenInclude(r => r.ArrivalAirport); // Rotanın içindeki Varış Havalimanını getir

            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }
        [Authorize(Roles = "Admin")]

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Aircraft)
                .Include(f => f.Routes)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AircraftId"] = new SelectList(_context.Aircrafts, "AircraftId", "AircraftId");
            ViewData["RoutesId"] = new SelectList(_context.Routes, "RoutesId", "RoutesId");
            return View();
        }

        // POST: Flights/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("FlightId,RoutesId,AircraftId,DepartureTime,ArrivalTime,FlightStatus")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AircraftId"] = new SelectList(_context.Aircrafts, "AircraftId", "AircraftId", flight.AircraftId);
            ViewData["RoutesId"] = new SelectList(_context.Routes, "RoutesId", "RoutesId", flight.RoutesId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AircraftId"] = new SelectList(_context.Aircrafts, "AircraftId", "AircraftId", flight.AircraftId);
            ViewData["RoutesId"] = new SelectList(_context.Routes, "RoutesId", "RoutesId", flight.RoutesId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,RoutesId,AircraftId,DepartureTime,ArrivalTime,FlightStatus")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightId))
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
            ViewData["AircraftId"] = new SelectList(_context.Aircrafts, "AircraftId", "AircraftId", flight.AircraftId);
            ViewData["RoutesId"] = new SelectList(_context.Routes, "RoutesId", "RoutesId", flight.RoutesId);
            return View(flight);
        }

        // GET: Flights/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Aircraft)
                .Include(f => f.Routes)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightId == id);
        }
    }
}