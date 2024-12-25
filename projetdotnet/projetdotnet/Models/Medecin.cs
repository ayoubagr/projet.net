using System;
using System.Collections.Generic;

namespace GestionClinique.Models;

public partial class Medecin
{
    public int MedecinId { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string? Specialisation { get; set; }

    public string? Planning { get; set; }

    public int AdminId { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public virtual Administrateur Admin { get; set; } = null!;

    public virtual ICollection<RendezVous> RendezVous { get; set; } = new List<RendezVous>();
}
