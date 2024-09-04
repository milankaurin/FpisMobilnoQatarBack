using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobilnoQatarBack.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModelsForNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utakmice_Stadioni_StadionId",
                table: "Utakmice");

            migrationBuilder.AlterColumn<int>(
                name: "StadionId",
                table: "Utakmice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Utakmice_Stadioni_StadionId",
                table: "Utakmice",
                column: "StadionId",
                principalTable: "Stadioni",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utakmice_Stadioni_StadionId",
                table: "Utakmice");

            migrationBuilder.AlterColumn<int>(
                name: "StadionId",
                table: "Utakmice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Utakmice_Stadioni_StadionId",
                table: "Utakmice",
                column: "StadionId",
                principalTable: "Stadioni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
