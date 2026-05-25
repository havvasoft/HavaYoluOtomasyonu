using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HavaYoluOtomasyonu.Models;

namespace HavaYoluOtomasyonu.Controllers
{
    public class FlightCrewsController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public FlightCrewsController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: FlightCrews
        public async Task<IActionResult> Index()
        {
            var havayoluOtomasyonDbContext = _context.FlightCrews.Include(f => f.Flight).Include(f => f.Staff);
            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }

        // GET: FlightCrews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightCrew = await _context.FlightCrews
                .Include(f => f.Flight)
                .Include(f => f.Staff)
                .FirstOrDefaultAsync(m => m.FlightCrewId == id);
            if (flightCrew == null)
            {
                return NotFound();
            }

            return View(flightCrew);
        }

        // GET: FlightCrews/Create
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId");
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
            return View();
        }

        // POST: FlightCrews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightCrewId,FlightId,StaffId,AssigmentRole")] FlightCrew flightCrew)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightCrew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", flightCrew.FlightId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", flightCrew.StaffId);
            return View(flightCrew);
        }

        // GET: FlightCrews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightCrew = await _context.FlightCrews.FindAsync(id);
            if (flightCrew == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", flightCrew.FlightId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", flightCrew.StaffId);
            return View(flightCrew);
        }

        // POST: FlightCrews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightCrewId,FlightId,StaffId,AssigmentRole")] FlightCrew flightCrew)
        {
            if (id != flightCrew.FlightCrewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightCrew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightCrewExists(flightCrew.FlightCrewId))
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
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", flightCrew.FlightId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", flightCrew.StaffId);
            return View(flightCrew);
        }

        // GET: FlightCrews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightCrew = await _context.FlightCrews
                .Include(f => f.Flight)
                .Include(f => f.Staff)
                .FirstOrDefaultAsync(m => m.FlightCrewId == id);
            if (flightCrew == null)
            {
                return NotFound();
            }

            return View(flightCrew);
        }

        // POST: FlightCrews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightCrew = await _context.FlightCrews.FindAsync(id);
            if (flightCrew != null)
            {
                _context.FlightCrews.Remove(flightCrew);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightCrewExists(int id)
        {
            return _context.FlightCrews.Any(e => e.FlightCrewId == id);
        }
    }
}
