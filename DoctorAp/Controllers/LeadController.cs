using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoctorAp.Data;
using DoctorAp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DoctorAp.Controllers
    
{
    [Authorize]
    public class LeadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lead
        public async Task<IActionResult> Index()
        {
              return _context.BookingLead != null ? 
                          View(await _context.BookingLead.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BookingLead'  is null.");
        }

        // GET: Lead/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookingLead == null)
            {
                return NotFound();
            }

            var bookingLeadEntity = await _context.BookingLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingLeadEntity == null)
            {
                return NotFound();
            }

            return View(bookingLeadEntity);
        }

        // GET: Lead/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lead/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Mobile,Email,Time")] BookingLeadEntity bookingLeadEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingLeadEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookingLeadEntity);
        }

        // GET: Lead/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookingLead == null)
            {
                return NotFound();
            }

            var bookingLeadEntity = await _context.BookingLead.FindAsync(id);
            if (bookingLeadEntity == null)
            {
                return NotFound();
            }
            return View(bookingLeadEntity);
        }

        // POST: Lead/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Mobile,Email,Time")] BookingLeadEntity bookingLeadEntity)
        {
            if (id != bookingLeadEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingLeadEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingLeadEntityExists(bookingLeadEntity.Id))
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
            return View(bookingLeadEntity);
        }

        // GET: Lead/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookingLead == null)
            {
                return NotFound();
            }

            var bookingLeadEntity = await _context.BookingLead
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingLeadEntity == null)
            {
                return NotFound();
            }

            return View(bookingLeadEntity);
        }

        // POST: Lead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookingLead == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BookingLead'  is null.");
            }
            var bookingLeadEntity = await _context.BookingLead.FindAsync(id);
            if (bookingLeadEntity != null)
            {
                _context.BookingLead.Remove(bookingLeadEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingLeadEntityExists(int id)
        {
          return (_context.BookingLead?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
