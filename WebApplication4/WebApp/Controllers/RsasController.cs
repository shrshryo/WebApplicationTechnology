using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CryptoRsa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;

namespace WebApp.Controllers
{
    [Authorize]
    public class RsasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RsasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim?.Value ?? "";
        }

        // GET: Rsas
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            return View(await _context.Rsas.Where(c=> c.UserId == userId).ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,FirstPrimeNum,SecondPrimeNum,MessageString,CipherTextString")] Rsa rsa)
        {

            if (!isPrime(rsa.FirstPrimeNum))
            {
                ModelState.AddModelError("FirstPrimeNum", "Invalid");
            }
            
            if (!isPrime(rsa.SecondPrimeNum))
            {
                ModelState.AddModelError("SecondPrimeNum", "Invalid");
            }

            if (rsa.MessageString.Trim() == "")
            {
                ModelState.AddModelError("MessageString", "Invalid");
            }

            if (ModelState.IsValid)
            {
                rsa.CipherTextString =
                    RsaCodec.calc((ulong) rsa.FirstPrimeNum, (ulong) rsa.SecondPrimeNum, rsa.MessageString);
                rsa.UserId = GetUserId();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstPrimeNum,SecondPrimeNum,MessageString,CipherTextString")] Rsa rsa)
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

        public static bool isPrime(int num)
        {
            if (num < 2)
            {
                return false;
            }
            for (int i = 2; i <= num/2; i++)
            {
                if (num % i == 0)
                {
                    Console.WriteLine($"{num} is not a prime number!");
                    return false;
                }
            }
            return true;
        }
        
    }
}
