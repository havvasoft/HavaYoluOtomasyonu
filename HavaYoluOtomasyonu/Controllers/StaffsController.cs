
using HavaYoluOtomasyonu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[Authorize(Roles = "Admin")]
public class StaffsController : Controller
{
    private readonly HavayoluOtomasyonDbContext _context;

    public StaffsController(HavayoluOtomasyonDbContext context)
    {
        _context = context;
    }

    // GET: STAFFS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Staff.ToListAsync());
    }

    // GET: STAFFS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var staff = await _context.Staff
            .FirstOrDefaultAsync(m => m.StaffId == id);
        if (staff == null)
        {
            return NotFound();
        }

        return View(staff);
    }

    // GET: STAFFS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: STAFFS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("StaffId,FirstName,LastName,Email,Phone,Password,HireDate,RoleId,EmploymentStatus,Bookings,FlightCrews,MaintenanceLogs,Role,TechnicalCertifications")] Staff staff)
    {
        if (ModelState.IsValid)
        {
            _context.Add(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(staff);
    }

    // GET: STAFFS/Edit/5
    public async Task<IActionResult> Edit(int? id) // staffid yerine id oldu
    {
        if (id == null)
        {
            return NotFound();
        }

        var staff = await _context.Staff.FindAsync(id);
        if (staff == null)
        {
            return NotFound();
        }
        return View(staff);
    }

    // POST: STAFFS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("StaffId,FirstName,LastName,Email,Phone,Password,HireDate,RoleId,EmploymentStatus,Bookings,FlightCrews,MaintenanceLogs,Role,TechnicalCertifications")] Staff staff)
    {
        if (id != staff.StaffId) // staffid yerine id oldu
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(staff);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(staff.StaffId))
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
        return View(staff);
    }

    // GET: STAFFS/Delete/5
    public async Task<IActionResult> Delete(int? id) // staffid yerine id oldu
    {
        if (id == null)
        {
            return NotFound();
        }

        var staff = await _context.Staff
            .FirstOrDefaultAsync(m => m.StaffId == id);
        if (staff == null)
        {
            return NotFound();
        }

        return View(staff);
    }

    // POST: STAFFS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id) // staffid yerine id oldu
    {
        var staff = await _context.Staff.FindAsync(id);
        if (staff != null)
        {
            _context.Staff.Remove(staff);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StaffExists(int id) // staffid yerine id oldu
    {
        return _context.Staff.Any(e => e.StaffId == id);
    }
}