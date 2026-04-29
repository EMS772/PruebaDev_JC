using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaDev_JC.Migrations
{
    /// <inheritdoc />
    public partial class ConsultaLogAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultaLog",
                columns: table => new
                {
                    IdConsulta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaConsulta = table.Column<DateTime>(type: "datetime", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Meses = table.Column<int>(type: "int", nullable: false),
                    ValorCuota = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    IP_de_Consulta = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    MensajeError = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaLog", x => x.IdConsulta);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultaLog");
        }
    }
}
