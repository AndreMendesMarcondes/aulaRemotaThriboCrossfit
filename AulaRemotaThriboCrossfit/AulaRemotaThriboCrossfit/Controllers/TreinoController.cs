using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AulaRemotaThriboCrossfit.Data;
using AulaRemotaThriboCrossfit.Models;

namespace AulaRemotaThriboCrossfit.Controllers
{
    public class TreinoController : Controller
    {
        private readonly AulaRemotaThriboCrossfitContext _context;

        public TreinoController(AulaRemotaThriboCrossfitContext context)
        {
            _context = context;
        }

        // GET: Treino
        public async Task<IActionResult> Index()
        {
            return View(await _context.Treino.ToListAsync());
        }

        // GET: Treino/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // GET: Treino/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Treino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dia")] Treino treino)
        {
            if (ModelState.IsValid)
            {
                treino.Id = Guid.NewGuid();
                _context.Add(treino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treino);
        }

        // GET: Treino/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }
            return View(treino);
        }

        // POST: Treino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Dia")] Treino treino)
        {
            if (id != treino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreinoExists(treino.Id))
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
            return View(treino);
        }

        // GET: Treino/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treino
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // POST: Treino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var treino = await _context.Treino.FindAsync(id);
            _context.Treino.Remove(treino);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreinoExists(Guid id)
        {
            return _context.Treino.Any(e => e.Id == id);
        }
    }
}
