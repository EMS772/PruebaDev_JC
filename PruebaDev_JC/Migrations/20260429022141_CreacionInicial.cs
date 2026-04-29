using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaDev_JC.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TasasEdad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Tasa = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasasEdad", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TasasEdad",
                columns: new[] { "Id", "Edad", "Tasa" },
                values: new object[,]
                {
                    { 1, 18, 1.20m },
                    { 2, 19, 1.18m },
                    { 3, 20, 1.16m },
                    { 4, 21, 1.14m },
                    { 5, 22, 1.12m },
                    { 6, 23, 1.10m },
                    { 7, 24, 1.08m },
                    { 8, 25, 1.05m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasasEdad");
        }
    }
}
