using HavaYoluOtomasyonu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            // Sisteme giren kişinin Rolünü ve ID'sini alıyoruz
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // YENİ: Bilet sayfasında sergilemek için yaklaşan uçuşları çekiyoruz
            ViewBag.YaklasanUcuslar = await _context.Flights
                .Include(f => f.Routes).ThenInclude(r => r.DepartureAirport)
                .Include(f => f.Routes).ThenInclude(r => r.ArrivalAirport)
                .Where(f => f.DepartureTime > DateTime.Now)
                .OrderBy(f => f.DepartureTime)
                .Take(6)
                .ToListAsync();

            // Eğer giren kişi Admin veya Yer Hizmetleri ise TÜM bilet listesini getir
            if (userRole == "Admin" || userRole == "YerHizmetleri")
            {
                return View(await _context.Tickets.Include(t => t.Passenger).Include(t => t.Flight).ToListAsync());
            }
            // Eğer giren kişi standart Müşteri ise, SADECE kendi biletlerini getir
            else
            {
                var myTickets = await _context.Tickets
                    .Include(t => t.Passenger)
                    .Include(t => t.Flight)
                    .Where(t => t.PassengerId.ToString() == userId)
                    .ToListAsync();

                return View(myTickets);
            }
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
