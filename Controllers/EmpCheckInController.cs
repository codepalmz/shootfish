using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CheckIn.Data;
using CheckIn.Models;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.AspNetCore.Authorization;

namespace CheckIn.Controllers
{
    
    public class EmpCheckInController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpCheckInController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmpCheckIn
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmpCheckIn.Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmpCheckIn/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empCheckIn = await _context.EmpCheckIn
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmpCheckInId == id);
            if (empCheckIn == null)
            {
                return NotFound();
            }

            return View(empCheckIn);
        }

        // GET: EmpCheckIn/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeId", "FirstName");
            return View();
        }

        // POST: EmpCheckIn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpCheckInId,EmployeeID,CreateDate")] EmpCheckIn empCheckIn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empCheckIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", empCheckIn.EmployeeID);
            
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Dammy", "adeogundamilolaaminat@gmail.com"));

            message.To.Add(new MailboxAddress("Ammy", "aminatstudies@gmail.com"));

            message.Subject = "I learnt to send an email using asp.net core";
            
            message.Body = new TextPart("plain")
            {
                Text = "I'm using MailKitNuget Package to send email easily"
            };

            using(var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("adeogundamilolaaminat@gmail.com", "aminatstudies@gmail.com");

                client.Send(message);

                client.Disconnect(true);
            }

            return View(empCheckIn);
        }

        // GET: EmpCheckIn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empCheckIn = await _context.EmpCheckIn.FindAsync(id);
            if (empCheckIn == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", empCheckIn.EmployeeID);
            return View(empCheckIn);
        }

        // POST: EmpCheckIn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpCheckInId,EmployeeID,CreateDate")] EmpCheckIn empCheckIn)
        {
            if (id != empCheckIn.EmpCheckInId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empCheckIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpCheckInExists(empCheckIn.EmpCheckInId))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "EmployeeId", "FirstName", empCheckIn.EmployeeID);
            return View(empCheckIn);
        }

        // GET: EmpCheckIn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empCheckIn = await _context.EmpCheckIn
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmpCheckInId == id);
            if (empCheckIn == null)
            {
                return NotFound();
            }

            return View(empCheckIn);
        }

        // POST: EmpCheckIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empCheckIn = await _context.EmpCheckIn.FindAsync(id);
            _context.EmpCheckIn.Remove(empCheckIn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpCheckInExists(int id)
        {
            return _context.EmpCheckIn.Any(e => e.EmpCheckInId == id);
        }
    }
}
