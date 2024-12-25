using System;
using System.Collections.Generic;

namespace GestionClinique.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? HistoriqueMedical { get; set; }

    public int AdminId { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public virtual Administrateur Admin { get; set; } = null!;

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    public virtual ICollection<RendezVous> RendezVous { get; set; } = new List<RendezVous>();
}
