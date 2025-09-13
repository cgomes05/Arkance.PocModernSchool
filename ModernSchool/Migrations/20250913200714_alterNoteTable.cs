using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernSchool.Migrations
{
    /// <inheritdoc />
    public partial class alterNoteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateNote",
                table: "Notes",
                newName: "DateAddNote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAddNote",
                table: "Notes",
                newName: "DateNote");
        }
    }
}
