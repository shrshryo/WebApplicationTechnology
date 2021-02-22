using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class RsaResultsesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RsaResultsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RsaResultses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RsaResultses.Include(r => r.RSA);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RsaResultses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rsaResults = await _context.RsaResultses
                .Include(r => r.RSA)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsaResults == null)
            {
                return NotFound();
            }

            return View(rsaResults);
        }

        // GET: RsaResultses/Create
        public IActionResult Create()
        {
            ViewData["RsaId"] = new SelectList(_context.Rsas, "Id", "Id");
            return View();
        }

        // POST: RsaResultses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Result,RsaId")] RsaResults rsaResults)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rsaResults);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RsaId"] = new SelectList(_context.Rsas, "Id", "Id", rsaResults.RsaId);
            return View(rsaResults);
        }

        // GET: RsaResultses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rsaResults = await _context.RsaResultses.FindAsync(id);
            if (rsaResults == null)
            {
                return NotFound();
            }
            ViewData["RsaId"] = new SelectList(_context.Rsas, "Id", "Id", rsaResults.RsaId);
            return View(rsaResults);
        }

        // POST: RsaResultses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Result,RsaId")] RsaResults rsaResults)
        {
            if (id != rsaResults.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rsaResults);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RsaResultsExists(rsaResults.Id))
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
            ViewData["RsaId"] = new SelectList(_context.Rsas, "Id", "Id", rsaResults.RsaId);
            return View(rsaResults);
        }

        // GET: RsaResultses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rsaResults = await _context.RsaResultses
                .Include(r => r.RSA)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsaResults == null)
            {
                return NotFound();
            }

            return View(rsaResults);
        }

        // POST: RsaResultses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rsaResults = await _context.RsaResultses.FindAsync(id);
            _context.RsaResultses.Remove(rsaResults);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RsaResultsExists(int id)
        {
            return _context.RsaResultses.Any(e => e.Id == id);
        }
    }
}
