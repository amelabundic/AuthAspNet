using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSigurnost.Models;

namespace WebSigurnost.Controllers
{
    public class KomentarController : Controller
    {
        
        private readonly KomentarContext _context;
        private readonly UserManager<ApplicationUser> _UserManager;

        public KomentarController(KomentarContext context, UserManager<ApplicationUser> UserManager)
        {
            _context = context;
            _UserManager = UserManager;
        }

        // GET: Komentar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Komentari.ToListAsync());
        }

        // GET: Komentar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentari
                .SingleOrDefaultAsync(m => m.KomentarId == id);
            if (komentar == null)
            {
                return NotFound();
            }

           
            return View(komentar);
        }

        // GET: Komentar/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Komentar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("KomentarId,Autor,Naslov,Sadrzaj")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(komentar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(komentar);
        }

        // GET: Komentar/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentari.SingleOrDefaultAsync(m => m.KomentarId == id);
            if (komentar == null)
            {
                return NotFound();
            }
            return View(komentar);
        }

        // POST: Komentar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("KomentarId,Autor,Naslov,Sadrzaj")] Komentar komentar)
        {
            if (id != komentar.KomentarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(komentar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KomentarExists(komentar.KomentarId))
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
            return View(komentar);
        }

        // GET: Komentar/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentari
                .SingleOrDefaultAsync(m => m.KomentarId == id);
            if (komentar == null)
            {
                return NotFound();
            }
            if (komentar.Autor != _UserManager.GetUserName(User) && _UserManager.GetUserName(User) !="admin@gmail.com")
            {
                return RedirectToAction("Index");
            }
            return View(komentar);
        }

        // POST: Komentar/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var komentar = await _context.Komentari.SingleOrDefaultAsync(m => m.KomentarId == id);
            if (komentar.Autor != _UserManager.GetUserName(User) && _UserManager.GetUserName(User) != "admin@gmail.com")
            {
                return RedirectToAction("Index");
            }
            _context.Komentari.Remove(komentar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KomentarExists(int id)
        {
            return _context.Komentari.Any(e => e.KomentarId == id);
        }
    }
}
