using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBank2.Models;

namespace MVCBank2.Controllers
{
    public class NewaccountsController : Controller
    {
        private readonly BankContext _context;

        public NewaccountsController(BankContext context)
        {
            _context = context;
        }

        // GET: Newaccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Newaccounts.ToListAsync());
        }

        // GET: Newaccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newaccount = await _context.Newaccounts
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (newaccount == null)
            {
                return NotFound();
            }

            return View(newaccount);
        }

        // GET: Newaccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Newaccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumber,CustomerName,CustomerAddress,CurrentBalance")] Newaccount newaccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newaccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newaccount);
        }

        // GET: Newaccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newaccount = await _context.Newaccounts.FindAsync(id);
            if (newaccount == null)
            {
                return NotFound();
            }
            return View(newaccount);
        }

        // POST: Newaccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountNumber,CustomerName,CustomerAddress,CurrentBalance")] Newaccount newaccount)
        {
            if (id != newaccount.AccountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newaccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewaccountExists(newaccount.AccountNumber))
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
            return View(newaccount);
        }

        // GET: Newaccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newaccount = await _context.Newaccounts
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (newaccount == null)
            {
                return NotFound();
            }

            return View(newaccount);
        }

        // POST: Newaccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newaccount = await _context.Newaccounts.FindAsync(id);
            _context.Newaccounts.Remove(newaccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewaccountExists(int id)
        {
            return _context.Newaccounts.Any(e => e.AccountNumber == id);
        }
    }
}
