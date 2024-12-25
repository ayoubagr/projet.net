using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetdotnet.Migrations
{
    /// <inheritdoc />
    public partial class gestionClinique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrateur",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DateModification = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Administ__719FE488C4EF4BF5", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Medecin",
                columns: table => new
                {
                    MedecinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Specialisation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Planning = table.Column<string>(type: "text", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DateModification = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Medecin__69D27AFB2C2326D1", x => x.MedecinId);
                    table.ForeignKey(
                        name: "FK__Medecin__AdminId__5DCAEF64",
                        column: x => x.AdminId,
                        principalTable: "Administrateur",
                        principalColumn: "AdminId");
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HistoriqueMedical = table.Column<string>(type: "text", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DateModification = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patient__970EC3661F993992", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK__Patient__AdminId__5812160E",
                        column: x => x.AdminId,
                        principalTable: "Administrateur",
                        principalColumn: "AdminId");
                });

            migrationBuilder.CreateTable(
                name: "RendezVous",
                columns: table => new
                {
                    RendezVousId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Heure = table.Column<TimeOnly>(type: "time", nullable: false),
                    MedecinId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DateModification = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RendezVo__C4B748C791A83405", x => x.RendezVousId);
                    table.ForeignKey(
                        name: "FK__RendezVou__Admin__6477ECF3",
                        column: x => x.AdminId,
                        principalTable: "Administrateur",
                        principalColumn: "AdminId");
                    table.ForeignKey(
                        name: "FK__RendezVou__Medec__628FA481",
                        column: x => x.MedecinId,
                        principalTable: "Medecin",
                        principalColumn: "MedecinId");
                    table.ForeignKey(
                        name: "FK__RendezVou__Patie__6383C8BA",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    FactureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Montant = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    RendezVousId = table.Column<int>(type: "int", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DateModification = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Facture__511BBA60ED8606D4", x => x.FactureId);
                    table.ForeignKey(
                        name: "FK__Facture__AdminId__6D0D32F4",
                        column: x => x.AdminId,
                        principalTable: "Administrateur",
                        principalColumn: "AdminId");
                    table.ForeignKey(
                        name: "FK__Facture__Patient__6B24EA82",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId");
                    table.ForeignKey(
                        name: "FK__Facture__RendezV__6C190EBB",
                        column: x => x.RendezVousId,
                        principalTable: "RendezVous",
                        principalColumn: "RendezVousId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facture_AdminId",
                table: "Facture",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_PatientId",
                table: "Facture",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_RendezVousId",
                table: "Facture",
                column: "RendezVousId");

            migrationBuilder.CreateIndex(
                name: "IX_Medecin_AdminId",
                table: "Medecin",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_AdminId",
                table: "Patient",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_AdminId",
                table: "RendezVous",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_MedecinId",
                table: "RendezVous",
                column: "MedecinId");

            migrationBuilder.CreateIndex(
                name: "IX_RendezVous_PatientId",
                table: "RendezVous",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropTable(
                name: "RendezVous");

            migrationBuilder.DropTable(
                name: "Medecin");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Administrateur");
        }
    }
}
