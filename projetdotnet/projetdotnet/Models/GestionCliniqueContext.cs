using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionClinique.Models;

public partial class GestionCliniqueContext : DbContext
{
    public GestionCliniqueContext()
    {
    }

    public GestionCliniqueContext(DbContextOptions<GestionCliniqueContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrateur> Administrateurs { get; set; }

    public virtual DbSet<Facture> Factures { get; set; }

    public virtual DbSet<Medecin> Medecins { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<RendezVous> RendezVous { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HASSAN\\SQLEXPRESS01;Database=GestionClinique;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrateur>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__719FE488C4EF4BF5");

            entity.ToTable("Administrateur", tb => tb.HasTrigger("TRG_UpdateAdminModificationDate"));

            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModification)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.Prenom).HasMaxLength(100);
        });

        modelBuilder.Entity<Facture>(entity =>
        {
            entity.HasKey(e => e.FactureId).HasName("PK__Facture__511BBA60ED8606D4");

            entity.ToTable("Facture", tb => tb.HasTrigger("TRG_UpdateFactureModificationDate"));

            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModification)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Admin).WithMany(p => p.Factures)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facture__AdminId__6D0D32F4");

            entity.HasOne(d => d.Patient).WithMany(p => p.Factures)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facture__Patient__6B24EA82");

            entity.HasOne(d => d.RendezVous).WithMany(p => p.Factures)
                .HasForeignKey(d => d.RendezVousId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Facture__RendezV__6C190EBB");
        });

        modelBuilder.Entity<Medecin>(entity =>
        {
            entity.HasKey(e => e.MedecinId).HasName("PK__Medecin__69D27AFB2C2326D1");

            entity.ToTable("Medecin", tb => tb.HasTrigger("TRG_UpdateMedecinModificationDate"));

            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModification)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.Planning).HasColumnType("text");
            entity.Property(e => e.Prenom).HasMaxLength(100);
            entity.Property(e => e.Specialisation).HasMaxLength(100);

            entity.HasOne(d => d.Admin).WithMany(p => p.Medecins)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Medecin__AdminId__5DCAEF64");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patient__970EC3661F993992");

            entity.ToTable("Patient", tb => tb.HasTrigger("TRG_UpdatePatientModificationDate"));

            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModification)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.HistoriqueMedical).HasColumnType("text");
            entity.Property(e => e.Nom).HasMaxLength(100);
            entity.Property(e => e.Prenom).HasMaxLength(100);
            entity.Property(e => e.Telephone).HasMaxLength(20);

            entity.HasOne(d => d.Admin).WithMany(p => p.Patients)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patient__AdminId__5812160E");
        });

        modelBuilder.Entity<RendezVous>(entity =>
        {
            entity.HasKey(e => e.RendezVousId).HasName("PK__RendezVo__C4B748C791A83405");

            entity.ToTable(tb => tb.HasTrigger("TRG_UpdateRendezVousModificationDate"));

            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateModification)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Admin).WithMany(p => p.RendezVous)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RendezVou__Admin__6477ECF3");

            entity.HasOne(d => d.Medecin).WithMany(p => p.RendezVous)
                .HasForeignKey(d => d.MedecinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RendezVou__Medec__628FA481");

            entity.HasOne(d => d.Patient).WithMany(p => p.RendezVous)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RendezVou__Patie__6383C8BA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
