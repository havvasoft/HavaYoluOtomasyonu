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
    public class MaintenanceLogsController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public MaintenanceLogsController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: MaintenanceLogs
        public async Task<IActionResult> Index()
        {
            var havayoluOtomasyonDbContext = _context.MaintenanceLogs.Include(m => m.Aircraft).Include(m => m.LeadEngineer);
            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }

        // GET: MaintenanceLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceLog = await _context.MaintenanceLogs
                .Include(m => m.Aircraft)
                .Include(m => m.LeadEngineer)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (maintenanceLog == null)
            {
                return NotFound();
            }

            return View(maintenanceLog);
        }

        // GET: MaintenanceLogs/Create
        public IActionResult Create()
        {
            ViewData["AircraftId"] = new SelectList(_context.Aircrafts, "AircraftId", "AircraftId");
            ViewData["LeadEngineerId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
            return View();
        }

        // POST: MaintenanceLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogId,AircraftId,LeadEngineerId,MaintenanceType,Description,MaintenanceDate")] MaintenanceLog maintenanceLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenanceLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AircraftId"] = new SelectList(_context.Aircrafts, "AircraftId", "AircraftId", maintenanceLog.AircraftId);
            ViewData["LeadEngineerId"] = new SelectList(_context.Staff, "StaffId", "StaffId", maintenanceLog.LeadEngineerId);
            return View(maintenanceLog);
        }

        // GET: MaintenanceLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceLog = await _context.MaintenanceLogs.FindAsync(id);
            if (maintenanceLog == null)
            {
                return NotFound();
            }
            ViewData["AircraftId"] = new SelectList(_context.Aircrafts, "AircraftId", "AircraftId", maintenanceLog.AircraftId);
            ViewData["LeadEngineerId"] = new SelectList(_context.Staff, "StaffId", "StaffId", maintenanceLog.LeadEngineerId);
            return View(maintenanceLog);
        }

        // POST: MaintenanceLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogId,AircraftId,LeadEngineerId,MaintenanceType,Description,MaintenanceDate")] MaintenanceLog maintenanceLog)
        {
            if (id != maintenanceLog.LogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenanceLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceLogExists(maintenanceLog.LogId))
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
            ViewData["AircraftId"] = new SelectList(_context.Aircrafts, "AircraftId", "AircraftId", maintenanceLog.AircraftId);
            ViewData["LeadEngineerId"] = new SelectList(_context.Staff, "StaffId", "StaffId", maintenanceLog.LeadEngineerId);
            return View(maintenanceLog);
        }

        // GET: MaintenanceLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceLog = await _context.MaintenanceLogs
                .Include(m => m.Aircraft)
                .Include(m => m.LeadEngineer)
                .FirstOrDefaultAsync(m => m.LogId == id);
            if (maintenanceLog == null)
            {
                return NotFound();
            }

            return View(maintenanceLog);
        }

        // POST: MaintenanceLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceLog = await _context.MaintenanceLogs.FindAsync(id);
            if (maintenanceLog != null)
            {
                _context.MaintenanceLogs.Remove(maintenanceLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceLogExists(int id)
        {
            return _context.MaintenanceLogs.Any(e => e.LogId == id);
        }
    }
}
