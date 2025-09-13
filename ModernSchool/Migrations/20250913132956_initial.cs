using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModernSchool.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    MatiereId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.MatiereId);
                });

            migrationBuilder.CreateTable(
                name: "Profs",
                columns: table => new
                {
                    ProfId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<char>(type: "character(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profs", x => x.ProfId);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClasseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomNiveau = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ProfPrincipalId = table.Column<int>(type: "integer", nullable: false),
                    DatCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DteUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClasseId);
                    table.ForeignKey(
                        name: "FK_Classes_Profs_ProfPrincipalId",
                        column: x => x.ProfPrincipalId,
                        principalTable: "Profs",
                        principalColumn: "ProfId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enseignes",
                columns: table => new
                {
                    ProfId = table.Column<int>(type: "integer", nullable: false),
                    MatiereId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseignes", x => new { x.ProfId, x.MatiereId });
                    table.ForeignKey(
                        name: "FK_Enseignes_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "MatiereId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enseignes_Profs_ProfId",
                        column: x => x.ProfId,
                        principalTable: "Profs",
                        principalColumn: "ProfId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    ClasseId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "ClasseId");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Valeur = table.Column<decimal>(type: "numeric", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    MatiereId = table.Column<int>(type: "integer", nullable: false),
                    DateNote = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "MatiereId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ProfPrincipalId",
                table: "Classes",
                column: "ProfPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignes_MatiereId",
                table: "Enseignes",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_MatiereId",
                table: "Notes",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_StudentId",
                table: "Notes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClasseId",
                table: "Students",
                column: "ClasseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enseignes");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Profs");
        }
    }
}
