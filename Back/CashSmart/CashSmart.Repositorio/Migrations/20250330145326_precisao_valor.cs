using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashSmart.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class precisao_valor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Transacoes",
                type: "decimal(24,2)",
                precision: 24,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldPrecision: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Parcelas",
                type: "decimal(24,2)",
                precision: 24,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldPrecision: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Transacoes",
                type: "decimal(2,2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(24,2)",
                oldPrecision: 24,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Parcelas",
                type: "decimal(2,2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(24,2)",
                oldPrecision: 24,
                oldScale: 2);
        }
    }
}
