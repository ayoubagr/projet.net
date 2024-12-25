using System;
using System.Collections.Generic;

namespace GestionClinique.Models;

public partial class RendezVous
{
    public int RendezVousId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Heure { get; set; }

    public int MedecinId { get; set; }

    public int PatientId { get; set; }

    public string? Status { get; set; }

    public int AdminId { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public virtual Administrateur Admin { get; set; } = null!;

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    public virtual Medecin Medecin { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
