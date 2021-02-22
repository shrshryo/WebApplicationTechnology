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
    public class DiffieHellmansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiffieHellmansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiffieHellmans
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiffieHellmans.ToListAsync());
        }

        // GET: DiffieHellmans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diffieHellman = await _context.DiffieHellmans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diffieHellman == null)
            {
                return NotFound();
            }

            return View(diffieHellman);
        }

        // GET: DiffieHellmans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiffieHellmans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrimeNum,CommonNum,SecretOne,SecretTwo")] DiffieHellman diffieHellman)
        {
            if (diffieHellman.PrimeNum < 2)
            {
                ModelState.AddModelError("PrimeNum", "Cannot be smaller than 2");
            }

            int flag = 0;
            int m = diffieHellman.PrimeNum / 2;
            for (int i = 2; i <= m; i++)
            {
                if (diffieHellman.PrimeNum % i == 0)
                {
                    Console.WriteLine($"{diffieHellman.PrimeNum} is not a prime number!"); 
                    flag = 1;
                    break;
                }
            }

            if (flag == 1)
            {
                ModelState.AddModelError("PrimeNum", "Not a prime");
            }
            
            if (diffieHellman.CommonNum <= 0)
            {
                ModelState.AddModelError("CommonNum", "Cannot be smaller than 0");
            }
            
            if (diffieHellman.SecretOne <= 0)
            {
                ModelState.AddModelError("SecretOne", "Cannot be smaller than 0");
            }
            
            if (diffieHellman.SecretTwo <= 0)
            {
                ModelState.AddModelError("SecretTwo", "Cannot be smaller than 0");
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(diffieHellman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diffieHellman);
        }

        // GET: DiffieHellmans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diffieHellman = await _context.DiffieHellmans.FindAsync(id);
            if (diffieHellman == null)
            {
                return NotFound();
            }
            return View(diffieHellman);
        }

        // POST: DiffieHellmans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrimeNum,CommonNum,SecretOne,SecretTwo")] DiffieHellman diffieHellman)
        {
            if (id != diffieHellman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diffieHellman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiffieHellmanExists(diffieHellman.Id))
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
            return View(diffieHellman);
        }

        // GET: DiffieHellmans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diffieHellman = await _context.DiffieHellmans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diffieHellman == null)
            {
                return NotFound();
            }

            return View(diffieHellman);
        }

        // POST: DiffieHellmans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diffieHellman = await _context.DiffieHellmans.FindAsync(id);
            _context.DiffieHellmans.Remove(diffieHellman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiffieHellmanExists(int id)
        {
            return _context.DiffieHellmans.Any(e => e.Id == id);
        }
    }
}
