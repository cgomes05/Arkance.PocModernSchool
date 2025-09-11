using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModernSchool.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyClasseProfesseur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentNote",
                table: "StudentNote");

            migrationBuilder.EnsureSchema(
                name: "[Valeur] >= 0 AND [Valeur] <= 20");

            migrationBuilder.RenameTable(
                name: "StudentNote",
                newName: "CK_Note_Valeur",
                newSchema: "[Valeur] >= 0 AND [Valeur] <= 20");

            migrationBuilder.RenameColumn(
                name: "Point",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                newName: "NoteId");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Students",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClasseClassId",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateNote",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MatiereId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valeur",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CK_Note_Valeur",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                column: "NoteId");

            migrationBuilder.CreateTable(
                name: "Matiere",
                columns: table => new
                {
                    MatiereId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere", x => x.MatiereId);
                });

            migrationBuilder.CreateTable(
                name: "Prof",
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
                    table.PrimaryKey("PK_Prof", x => x.ProfId);
                });

            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomNiveau = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ProfPrincipalId = table.Column<int>(type: "integer", nullable: false),
                    DatCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DteUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Classe_Prof_ProfPrincipalId",
                        column: x => x.ProfPrincipalId,
                        principalTable: "Prof",
                        principalColumn: "ProfId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enseigne",
                columns: table => new
                {
                    ProfId = table.Column<int>(type: "integer", nullable: false),
                    MatiereId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseigne", x => new { x.ProfId, x.MatiereId });
                    table.ForeignKey(
                        name: "FK_Enseigne_Matiere_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matiere",
                        principalColumn: "MatiereId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enseigne_Prof_ProfId",
                        column: x => x.ProfId,
                        principalTable: "Prof",
                        principalColumn: "ProfId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClasseClassId",
                table: "Students",
                column: "ClasseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CK_Note_Valeur_MatiereId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_CK_Note_Valeur_StudentId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Classe_ProfPrincipalId",
                table: "Classe",
                column: "ProfPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Enseigne_MatiereId",
                table: "Enseigne",
                column: "MatiereId");

            migrationBuilder.AddForeignKey(
                name: "FK_CK_Note_Valeur_Matiere_MatiereId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                column: "MatiereId",
                principalTable: "Matiere",
                principalColumn: "MatiereId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CK_Note_Valeur_Students_StudentId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classe_ClasseClassId",
                table: "Students",
                column: "ClasseClassId",
                principalTable: "Classe",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CK_Note_Valeur_Matiere_MatiereId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur");

            migrationBuilder.DropForeignKey(
                name: "FK_CK_Note_Valeur_Students_StudentId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classe_ClasseClassId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Classe");

            migrationBuilder.DropTable(
                name: "Enseigne");

            migrationBuilder.DropTable(
                name: "Matiere");

            migrationBuilder.DropTable(
                name: "Prof");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClasseClassId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CK_Note_Valeur",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur");

            migrationBuilder.DropIndex(
                name: "IX_CK_Note_Valeur_MatiereId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur");

            migrationBuilder.DropIndex(
                name: "IX_CK_Note_Valeur_StudentId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClasseClassId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DateNote",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur");

            migrationBuilder.DropColumn(
                name: "MatiereId",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur");

            migrationBuilder.DropColumn(
                name: "Valeur",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                table: "CK_Note_Valeur");

            migrationBuilder.RenameTable(
                name: "CK_Note_Valeur",
                schema: "[Valeur] >= 0 AND [Valeur] <= 20",
                newName: "StudentNote");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentNote",
                newName: "Point");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "StudentNote",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1)",
                oldMaxLength: 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentNote",
                table: "StudentNote",
                column: "Id");
        }
    }
}
