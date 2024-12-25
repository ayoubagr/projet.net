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
    public class RendezVousController : Controller
    {
        private readonly GestionCliniqueContext _context;

        public RendezVousController(GestionCliniqueContext context)
        {
            _context = context;
        }

        // GET: RendezVous
        public async Task<IActionResult> Index()
        {
            var gestionCliniqueContext = _context.RendezVous.Include(r => r.Admin).Include(r => r.Medecin).Include(r => r.Patient);
            return View(await gestionCliniqueContext.ToListAsync());
        }

        // GET: RendezVous/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rendezVou = await _context.RendezVous
                .Include(r => r.Admin)
                .Include(r => r.Medecin)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.RendezVousId == id);
            if (rendezVou == null)
            {
                return NotFound();
            }

            return View(rendezVou);
        }

        // GET: RendezVous/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId");
            ViewData["MedecinId"] = new SelectList(_context.Medecins, "MedecinId", "MedecinId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            return View();
        }

        // POST: RendezVous/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RendezVousId,Date,Heure,MedecinId,PatientId,Status,AdminId,DateCreation,DateModification")] RendezVous rendezVou)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rendezVou);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", rendezVou.AdminId);
            ViewData["MedecinId"] = new SelectList(_context.Medecins, "MedecinId", "MedecinId", rendezVou.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", rendezVou.PatientId);
            return View(rendezVou);
        }

        // GET: RendezVous/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rendezVou = await _context.RendezVous.FindAsync(id);
            if (rendezVou == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", rendezVou.AdminId);
            ViewData["MedecinId"] = new SelectList(_context.Medecins, "MedecinId", "MedecinId", rendezVou.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", rendezVou.PatientId);
            return View(rendezVou);
        }

        // POST: RendezVous/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RendezVousId,Date,Heure,MedecinId,PatientId,Status,AdminId,DateCreation,DateModification")] RendezVous rendezVou)
        {
            if (id != rendezVou.RendezVousId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rendezVou);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RendezVouExists(rendezVou.RendezVousId))
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
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", rendezVou.AdminId);
            ViewData["MedecinId"] = new SelectList(_context.Medecins, "MedecinId", "MedecinId", rendezVou.MedecinId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", rendezVou.PatientId);
            return View(rendezVou);
        }

        // GET: RendezVous/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rendezVou = await _context.RendezVous
                .Include(r => r.Admin)
                .Include(r => r.Medecin)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(m => m.RendezVousId == id);
            if (rendezVou == null)
            {
                return NotFound();
            }

            return View(rendezVou);
        }

        // POST: RendezVous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rendezVou = await _context.RendezVous.FindAsync(id);
            if (rendezVou != null)
            {
                _context.RendezVous.Remove(rendezVou);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RendezVouExists(int id)
        {
            return _context.RendezVous.Any(e => e.RendezVousId == id);
        }
    }
}
