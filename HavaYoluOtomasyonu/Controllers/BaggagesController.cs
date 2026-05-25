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
    public class BaggagesController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public BaggagesController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: Baggages
        public async Task<IActionResult> Index()
        {
            var havayoluOtomasyonDbContext = _context.Baggages.Include(b => b.Ticket);
            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }

        // GET: Baggages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baggage = await _context.Baggages
                .Include(b => b.Ticket)
                .FirstOrDefaultAsync(m => m.BaggageId == id);
            if (baggage == null)
            {
                return NotFound();
            }

            return View(baggage);
        }

        // GET: Baggages/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketId");
            return View();
        }

        // POST: Baggages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BaggageId,TicketId,Weight,BaggageTagNumber,Status")] Baggage baggage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baggage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketId", baggage.TicketId);
            return View(baggage);
        }

        // GET: Baggages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baggage = await _context.Baggages.FindAsync(id);
            if (baggage == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketId", baggage.TicketId);
            return View(baggage);
        }

        // POST: Baggages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BaggageId,TicketId,Weight,BaggageTagNumber,Status")] Baggage baggage)
        {
            if (id != baggage.BaggageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baggage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaggageExists(baggage.BaggageId))
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
            ViewData["TicketId"] = new SelectList(_context.Tickets, "TicketId", "TicketId", baggage.TicketId);
            return View(baggage);
        }

        // GET: Baggages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baggage = await _context.Baggages
                .Include(b => b.Ticket)
                .FirstOrDefaultAsync(m => m.BaggageId == id);
            if (baggage == null)
            {
                return NotFound();
            }

            return View(baggage);
        }

        // POST: Baggages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baggage = await _context.Baggages.FindAsync(id);
            if (baggage != null)
            {
                _context.Baggages.Remove(baggage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaggageExists(int id)
        {
            return _context.Baggages.Any(e => e.BaggageId == id);
        }
    }
}
