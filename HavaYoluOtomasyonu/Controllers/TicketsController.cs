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
    public class TicketsController : Controller
    {
        private readonly HavayoluOtomasyonDbContext _context;

        public TicketsController(HavayoluOtomasyonDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var havayoluOtomasyonDbContext = _context.Tickets.Include(t => t.Booking).Include(t => t.Fare).Include(t => t.Flight).Include(t => t.Passenger);
            return View(await havayoluOtomasyonDbContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Booking)
                .Include(t => t.Fare)
                .Include(t => t.Flight)
                .Include(t => t.Passenger)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "BookingId");
            ViewData["FareId"] = new SelectList(_context.Fares, "FareId", "FareId");
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId");
            ViewData["PassengerId"] = new SelectList(_context.Passengers, "PassengerId", "PassengerId");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,TicketNumber,FlightId,PassengerId,SeatNumber,ClassType,BookingId,FareId,IsCheckedIn,BaggageAllowance")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "BookingId", ticket.BookingId);
            ViewData["FareId"] = new SelectList(_context.Fares, "FareId", "FareId", ticket.FareId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", ticket.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Passengers, "PassengerId", "PassengerId", ticket.PassengerId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "BookingId", ticket.BookingId);
            ViewData["FareId"] = new SelectList(_context.Fares, "FareId", "FareId", ticket.FareId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", ticket.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Passengers, "PassengerId", "PassengerId", ticket.PassengerId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,TicketNumber,FlightId,PassengerId,SeatNumber,ClassType,BookingId,FareId,IsCheckedIn,BaggageAllowance")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            ViewData["BookingId"] = new SelectList(_context.Bookings, "BookingId", "BookingId", ticket.BookingId);
            ViewData["FareId"] = new SelectList(_context.Fares, "FareId", "FareId", ticket.FareId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", ticket.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Passengers, "PassengerId", "PassengerId", ticket.PassengerId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Booking)
                .Include(t => t.Fare)
                .Include(t => t.Flight)
                .Include(t => t.Passenger)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }
    }
}
