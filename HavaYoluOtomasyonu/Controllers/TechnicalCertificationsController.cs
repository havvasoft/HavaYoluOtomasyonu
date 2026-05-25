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
    public class TechnicalCertificationsController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public TechnicalCertificationsController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: TechnicalCertifications
        public async Task<IActionResult> Index()
        {
            var havayoluOtomasyonDbContext = _context.TechnicalCertifications.Include(t => t.Model).Include(t => t.Staff);
            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }

        // GET: TechnicalCertifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicalCertification = await _context.TechnicalCertifications
                .Include(t => t.Model)
                .Include(t => t.Staff)
                .FirstOrDefaultAsync(m => m.CertificationId == id);
            if (technicalCertification == null)
            {
                return NotFound();
            }

            return View(technicalCertification);
        }

        // GET: TechnicalCertifications/Create
        public IActionResult Create()
        {
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId");
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId");
            return View();
        }

        // POST: TechnicalCertifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CertificationId,StaffId,ModelId,LicenseType,ExpiryDate")] TechnicalCertification technicalCertification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technicalCertification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId", technicalCertification.ModelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", technicalCertification.StaffId);
            return View(technicalCertification);
        }

        // GET: TechnicalCertifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicalCertification = await _context.TechnicalCertifications.FindAsync(id);
            if (technicalCertification == null)
            {
                return NotFound();
            }
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId", technicalCertification.ModelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", technicalCertification.StaffId);
            return View(technicalCertification);
        }

        // POST: TechnicalCertifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CertificationId,StaffId,ModelId,LicenseType,ExpiryDate")] TechnicalCertification technicalCertification)
        {
            if (id != technicalCertification.CertificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technicalCertification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnicalCertificationExists(technicalCertification.CertificationId))
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
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId", technicalCertification.ModelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "StaffId", technicalCertification.StaffId);
            return View(technicalCertification);
        }

        // GET: TechnicalCertifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technicalCertification = await _context.TechnicalCertifications
                .Include(t => t.Model)
                .Include(t => t.Staff)
                .FirstOrDefaultAsync(m => m.CertificationId == id);
            if (technicalCertification == null)
            {
                return NotFound();
            }

            return View(technicalCertification);
        }

        // POST: TechnicalCertifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technicalCertification = await _context.TechnicalCertifications.FindAsync(id);
            if (technicalCertification != null)
            {
                _context.TechnicalCertifications.Remove(technicalCertification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnicalCertificationExists(int id)
        {
            return _context.TechnicalCertifications.Any(e => e.CertificationId == id);
        }
    }
}
