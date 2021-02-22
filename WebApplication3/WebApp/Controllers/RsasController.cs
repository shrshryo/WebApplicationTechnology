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
    public class RsasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RsasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rsas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rsas.ToListAsync());
        }

        // GET: Rsas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rsa = await _context.Rsas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsa == null)
            {
                return NotFound();
            }

            return View(rsa);
        }

        // GET: Rsas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rsas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstPrimeNum,SecondPrimeNum,Message,CipherText,MessageString,CipherTextString")] Rsa rsa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rsa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rsa);
        }

        // GET: Rsas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rsa = await _context.Rsas.FindAsync(id);
            if (rsa == null)
            {
                return NotFound();
            }
            return View(rsa);
        }

        // POST: Rsas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstPrimeNum,SecondPrimeNum,Message,CipherText,MessageString,CipherTextString")] Rsa rsa)
        {
            if (id != rsa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rsa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RsaExists(rsa.Id))
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
            return View(rsa);
        }

        // GET: Rsas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rsa = await _context.Rsas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsa == null)
            {
                return NotFound();
            }

            return View(rsa);
        }

        // POST: Rsas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rsa = await _context.Rsas.FindAsync(id);
            _context.Rsas.Remove(rsa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RsaExists(int id)
        {
            return _context.Rsas.Any(e => e.Id == id);
        }
    }
}
