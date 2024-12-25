using System;
using System.Collections.Generic;

namespace GestionClinique.Models;

public partial class Administrateur
{
    public int AdminId { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? DateCreation { get; set; }

    public DateTime? DateModification { get; set; }

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    public virtual ICollection<Medecin> Medecins { get; set; } = new List<Medecin>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<RendezVous> RendezVous { get; set; } = new List<RendezVous>();
}
