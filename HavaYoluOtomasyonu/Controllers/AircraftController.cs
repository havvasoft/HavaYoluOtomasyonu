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
    [Authorize(Roles = "Admin")]
    public class AircraftController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public AircraftController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: Aircraft
        public async Task<IActionResult> Index()
        {
            var havayoluOtomasyonDbContext = _context.Aircrafts.Include(a => a.Model);
            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }

        // GET: Aircraft/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .Include(a => a.Model)
                .FirstOrDefaultAsync(m => m.AircraftId == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // GET: Aircraft/Create
        public IActionResult Create()
        {
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId");
            return View();
        }

        // POST: Aircraft/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AircraftId,ModelId,ManufactureDate,TailNumber,Status")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aircraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId", aircraft.ModelId);
            return View(aircraft);
        }

        // GET: Aircraft/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId", aircraft.ModelId);
            return View(aircraft);
        }

        // POST: Aircraft/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AircraftId,ModelId,ManufactureDate,TailNumber,Status")] Aircraft aircraft)
        {
            if (id != aircraft.AircraftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftExists(aircraft.AircraftId))
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
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId", aircraft.ModelId);
            return View(aircraft);
        }

        // GET: Aircraft/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts
                .Include(a => a.Model)
                .FirstOrDefaultAsync(m => m.AircraftId == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft != null)
            {
                _context.Aircrafts.Remove(aircraft);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftExists(int id)
        {
            return _context.Aircrafts.Any(e => e.AircraftId == id);
        }
    }
}
