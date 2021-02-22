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
    public class DiffieHellmanResultsesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiffieHellmanResultsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiffieHellmanResultses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DiffieHellmanResultses.Include(d => d.Diffie_Hellman);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DiffieHellmanResultses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diffieHellmanResults = await _context.DiffieHellmanResultses
                .Include(d => d.Diffie_Hellman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diffieHellmanResults == null)
            {
                return NotFound();
            }

            return View(diffieHellmanResults);
        }

        // GET: DiffieHellmanResultses/Create
        public IActionResult Create()
        {
            ViewData["DiffieHellmanId"] = new SelectList(_context.DiffieHellmans, "Id", "Id");
            return View();
        }

        // POST: DiffieHellmanResultses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResultValue,DiffieHellmanId")] DiffieHellmanResults diffieHellmanResults)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diffieHellmanResults);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiffieHellmanId"] = new SelectList(_context.DiffieHellmans, "Id", "Id", diffieHellmanResults.DiffieHellmanId);
            return View(diffieHellmanResults);
        }

        // GET: DiffieHellmanResultses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diffieHellmanResults = await _context.DiffieHellmanResultses.FindAsync(id);
            if (diffieHellmanResults == null)
            {
                return NotFound();
            }
            ViewData["DiffieHellmanId"] = new SelectList(_context.DiffieHellmans, "Id", "Id", diffieHellmanResults.DiffieHellmanId);
            return View(diffieHellmanResults);
        }

        // POST: DiffieHellmanResultses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ResultValue,DiffieHellmanId")] DiffieHellmanResults diffieHellmanResults)
        {
            if (id != diffieHellmanResults.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diffieHellmanResults);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiffieHellmanResultsExists(diffieHellmanResults.Id))
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
            ViewData["DiffieHellmanId"] = new SelectList(_context.DiffieHellmans, "Id", "Id", diffieHellmanResults.DiffieHellmanId);
            return View(diffieHellmanResults);
        }

        // GET: DiffieHellmanResultses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diffieHellmanResults = await _context.DiffieHellmanResultses
                .Include(d => d.Diffie_Hellman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diffieHellmanResults == null)
            {
                return NotFound();
            }

            return View(diffieHellmanResults);
        }

        // POST: DiffieHellmanResultses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diffieHellmanResults = await _context.DiffieHellmanResultses.FindAsync(id);
            _context.DiffieHellmanResultses.Remove(diffieHellmanResults);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiffieHellmanResultsExists(int id)
        {
            return _context.DiffieHellmanResultses.Any(e => e.Id == id);
        }
    }
}
