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
    public class FlightStatusHistoriesController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public FlightStatusHistoriesController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: FlightStatusHistories
        public async Task<IActionResult> Index()
        {
            var havayoluOtomasyonDbContext = _context.FlightStatusHistories.Include(f => f.Flight);
            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }

        // GET: FlightStatusHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightStatusHistory = await _context.FlightStatusHistories
                .Include(f => f.Flight)
                .FirstOrDefaultAsync(m => m.HistoryId == id);
            if (flightStatusHistory == null)
            {
                return NotFound();
            }

            return View(flightStatusHistory);
        }

        // GET: FlightStatusHistories/Create
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId");
            return View();
        }

        // POST: FlightStatusHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryId,FlightId,Status,Reason,UpdateTime")] FlightStatusHistory flightStatusHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightStatusHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", flightStatusHistory.FlightId);
            return View(flightStatusHistory);
        }

        // GET: FlightStatusHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightStatusHistory = await _context.FlightStatusHistories.FindAsync(id);
            if (flightStatusHistory == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", flightStatusHistory.FlightId);
            return View(flightStatusHistory);
        }

        // POST: FlightStatusHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryId,FlightId,Status,Reason,UpdateTime")] FlightStatusHistory flightStatusHistory)
        {
            if (id != flightStatusHistory.HistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightStatusHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightStatusHistoryExists(flightStatusHistory.HistoryId))
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
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", flightStatusHistory.FlightId);
            return View(flightStatusHistory);
        }

        // GET: FlightStatusHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightStatusHistory = await _context.FlightStatusHistories
                .Include(f => f.Flight)
                .FirstOrDefaultAsync(m => m.HistoryId == id);
            if (flightStatusHistory == null)
            {
                return NotFound();
            }

            return View(flightStatusHistory);
        }

        // POST: FlightStatusHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightStatusHistory = await _context.FlightStatusHistories.FindAsync(id);
            if (flightStatusHistory != null)
            {
                _context.FlightStatusHistories.Remove(flightStatusHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightStatusHistoryExists(int id)
        {
            return _context.FlightStatusHistories.Any(e => e.HistoryId == id);
        }
    }
}
