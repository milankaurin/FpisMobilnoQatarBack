using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobilnoQatarBack.Migrations
{
    /// <inheritdoc />
    public partial class TIMiUtakmica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datum",
                table: "Utakmice");

            migrationBuilder.DropColumn(
                name: "Rezultat",
                table: "Utakmice");

            migrationBuilder.RenameColumn(
                name: "Vreme",
                table: "Utakmice",
                newName: "VremePocetka");

            migrationBuilder.AddColumn<int>(
                name: "Tim1Golovi",
                table: "Utakmice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tim2Golovi",
                table: "Utakmice",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tim1Golovi",
                table: "Utakmice");

            migrationBuilder.DropColumn(
                name: "Tim2Golovi",
                table: "Utakmice");

            migrationBuilder.RenameColumn(
                name: "VremePocetka",
                table: "Utakmice",
                newName: "Vreme");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "Utakmice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Rezultat",
                table: "Utakmice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
