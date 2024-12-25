using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionClinique.Models;

namespace projetdotnet.Controllers
{
    public class MedecinsController : Controller
    {
        private readonly GestionCliniqueContext _context;

        public MedecinsController(GestionCliniqueContext context)
        {
            _context = context;
        }

        // GET: Medecins
        public async Task<IActionResult> Index()
        {
            var gestionCliniqueContext = _context.Medecins.Include(m => m.Admin);
            return View(await gestionCliniqueContext.ToListAsync());
        }

        // GET: Medecins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medecin = await _context.Medecins
                .Include(m => m.Admin)
                .FirstOrDefaultAsync(m => m.MedecinId == id);
            if (medecin == null)
            {
                return NotFound();
            }

            return View(medecin);
        }

        // GET: Medecins/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId");
            return View();
        }

        // POST: Medecins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedecinId,Nom,Prenom,Specialisation,Planning,AdminId,DateCreation,DateModification")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medecin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", medecin.AdminId);
            return View(medecin);
        }

        // GET: Medecins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medecin = await _context.Medecins.FindAsync(id);
            if (medecin == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", medecin.AdminId);
            return View(medecin);
        }

        // POST: Medecins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedecinId,Nom,Prenom,Specialisation,Planning,AdminId,DateCreation,DateModification")] Medecin medecin)
        {
            if (id != medecin.MedecinId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medecin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedecinExists(medecin.MedecinId))
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
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", medecin.AdminId);
            return View(medecin);
        }

        // GET: Medecins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medecin = await _context.Medecins
                .Include(m => m.Admin)
                .FirstOrDefaultAsync(m => m.MedecinId == id);
            if (medecin == null)
            {
                return NotFound();
            }

            return View(medecin);
        }

        // POST: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medecin = await _context.Medecins.FindAsync(id);
            if (medecin != null)
            {
                _context.Medecins.Remove(medecin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedecinExists(int id)
        {
            return _context.Medecins.Any(e => e.MedecinId == id);
        }
    }
}
