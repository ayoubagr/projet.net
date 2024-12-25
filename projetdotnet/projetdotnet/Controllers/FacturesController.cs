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
    public class FacturesController : Controller
    {
        private readonly GestionCliniqueContext _context;

        public FacturesController(GestionCliniqueContext context)
        {
            _context = context;
        }

        // GET: Factures
        public async Task<IActionResult> Index()
        {
            var gestionCliniqueContext = _context.Factures.Include(f => f.Admin).Include(f => f.Patient).Include(f => f.RendezVous);
            return View(await gestionCliniqueContext.ToListAsync());
        }

        // GET: Factures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures
                .Include(f => f.Admin)
                .Include(f => f.Patient)
                .Include(f => f.RendezVous)
                .FirstOrDefaultAsync(m => m.FactureId == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // GET: Factures/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            ViewData["RendezVousId"] = new SelectList(_context.RendezVous, "RendezVousId", "RendezVousId");
            return View();
        }

        // POST: Factures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FactureId,Date,Montant,Status,PatientId,RendezVousId,AdminId,DateCreation,DateModification")] Facture facture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", facture.AdminId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", facture.PatientId);
            ViewData["RendezVousId"] = new SelectList(_context.RendezVous, "RendezVousId", "RendezVousId", facture.RendezVousId);
            return View(facture);
        }

        // GET: Factures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures.FindAsync(id);
            if (facture == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", facture.AdminId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", facture.PatientId);
            ViewData["RendezVousId"] = new SelectList(_context.RendezVous, "RendezVousId", "RendezVousId", facture.RendezVousId);
            return View(facture);
        }

        // POST: Factures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FactureId,Date,Montant,Status,PatientId,RendezVousId,AdminId,DateCreation,DateModification")] Facture facture)
        {
            if (id != facture.FactureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactureExists(facture.FactureId))
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
            ViewData["AdminId"] = new SelectList(_context.Administrateurs, "AdminId", "AdminId", facture.AdminId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", facture.PatientId);
            ViewData["RendezVousId"] = new SelectList(_context.RendezVous, "RendezVousId", "RendezVousId", facture.RendezVousId);
            return View(facture);
        }

        // GET: Factures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures
                .Include(f => f.Admin)
                .Include(f => f.Patient)
                .Include(f => f.RendezVous)
                .FirstOrDefaultAsync(m => m.FactureId == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // POST: Factures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facture = await _context.Factures.FindAsync(id);
            if (facture != null)
            {
                _context.Factures.Remove(facture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FactureExists(int id)
        {
            return _context.Factures.Any(e => e.FactureId == id);
        }
    }
}
