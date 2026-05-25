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
    public class FaresController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public FaresController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: Fares
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fares.ToListAsync());
        }

        // GET: Fares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fare = await _context.Fares
                .FirstOrDefaultAsync(m => m.FareId == id);
            if (fare == null)
            {
                return NotFound();
            }

            return View(fare);
        }

        // GET: Fares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FareId,FareBasicCode,Refundable,Changeable,CabinType")] Fare fare)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fare);
        }

        // GET: Fares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fare = await _context.Fares.FindAsync(id);
            if (fare == null)
            {
                return NotFound();
            }
            return View(fare);
        }

        // POST: Fares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FareId,FareBasicCode,Refundable,Changeable,CabinType")] Fare fare)
        {
            if (id != fare.FareId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FareExists(fare.FareId))
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
            return View(fare);
        }

        // GET: Fares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fare = await _context.Fares
                .FirstOrDefaultAsync(m => m.FareId == id);
            if (fare == null)
            {
                return NotFound();
            }

            return View(fare);
        }

        // POST: Fares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fare = await _context.Fares.FindAsync(id);
            if (fare != null)
            {
                _context.Fares.Remove(fare);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FareExists(int id)
        {
            return _context.Fares.Any(e => e.FareId == id);
        }
    }
}
