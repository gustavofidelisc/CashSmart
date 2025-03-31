using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashSmart.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class descricao_nao_obrigatoria_transacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Transacoes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Transacoes",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
