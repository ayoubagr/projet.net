
using System;
using System.Collections.Generic;

namespace GestionClinique.Models;

public partial class Facture
{
    public int FactureId { get; set; }

    public DateOnly Date { get; set; }

    public double Montant { get; set; }

    public string? Status { get; set; }

    public int PatientId { get; set; }

    public int RendezVousId { get; set; }

    public int AdminId { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public virtual Administrateur Admin { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual RendezVous RendezVous { get; set; } = null!;
}
