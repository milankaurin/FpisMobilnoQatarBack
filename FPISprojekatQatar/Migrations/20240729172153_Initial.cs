using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobilnoQatarBack.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeGrupe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stadioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeStadiona = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadioni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeTima = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zastavica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrupaId = table.Column<int>(type: "int", nullable: false),
                    BrojPoena = table.Column<int>(type: "int", nullable: false),
                    BrojPobeda = table.Column<int>(type: "int", nullable: false),
                    BrojPoraza = table.Column<int>(type: "int", nullable: false),
                    BrojNeresenih = table.Column<int>(type: "int", nullable: false),
                    BrojDatihGolova = table.Column<int>(type: "int", nullable: false),
                    BrojPrimljenihGolova = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timovi_Grupe_GrupaId",
                        column: x => x.GrupaId,
                        principalTable: "Grupe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utakmice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tim1Id = table.Column<int>(type: "int", nullable: false),
                    Tim2Id = table.Column<int>(type: "int", nullable: false),
                    Rezultat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Predato = table.Column<bool>(type: "bit", nullable: false),
                    StadionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utakmice_Stadioni_StadionId",
                        column: x => x.StadionId,
                        principalTable: "Stadioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utakmice_Timovi_Tim1Id",
                        column: x => x.Tim1Id,
                        principalTable: "Timovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Utakmice_Timovi_Tim2Id",
                        column: x => x.Tim2Id,
                        principalTable: "Timovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timovi_GrupaId",
                table: "Timovi",
                column: "GrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_StadionId",
                table: "Utakmice",
                column: "StadionId");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_Tim1Id",
                table: "Utakmice",
                column: "Tim1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_Tim2Id",
                table: "Utakmice",
                column: "Tim2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utakmice");

            migrationBuilder.DropTable(
                name: "Stadioni");

            migrationBuilder.DropTable(
                name: "Timovi");

            migrationBuilder.DropTable(
                name: "Grupe");
        }
    }
}
