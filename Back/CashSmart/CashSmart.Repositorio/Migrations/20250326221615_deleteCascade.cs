using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashSmart.Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class deleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcelas_Transacao_TransacaoID",
                table: "Parcelas");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Categorias_CategoriaId",
                table: "Transacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_FormasPagamento_FormaPagamentoId",
                table: "Transacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_Usuarios_UsuarioId",
                table: "Transacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacao",
                table: "Transacao");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "FormasPagamento");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Transacao");

            migrationBuilder.RenameTable(
                name: "Transacao",
                newName: "Transacoes");

            migrationBuilder.RenameIndex(
                name: "IX_Transacao_UsuarioId",
                table: "Transacoes",
                newName: "IX_Transacoes_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacao_FormaPagamentoId",
                table: "Transacoes",
                newName: "IX_Transacoes_FormaPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacao_CategoriaId",
                table: "Transacoes",
                newName: "IX_Transacoes_CategoriaId");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Transacoes",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacoes",
                table: "Transacoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcelas_Transacoes_TransacaoID",
                table: "Parcelas",
                column: "TransacaoID",
                principalTable: "Transacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Categorias_CategoriaId",
                table: "Transacoes",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_FormasPagamento_FormaPagamentoId",
                table: "Transacoes",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Usuarios_UsuarioId",
                table: "Transacoes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcelas_Transacoes_TransacaoID",
                table: "Parcelas");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Categorias_CategoriaId",
                table: "Transacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_FormasPagamento_FormaPagamentoId",
                table: "Transacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Usuarios_UsuarioId",
                table: "Transacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacoes",
                table: "Transacoes");

            migrationBuilder.RenameTable(
                name: "Transacoes",
                newName: "Transacao");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_UsuarioId",
                table: "Transacao",
                newName: "IX_Transacao_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_FormaPagamentoId",
                table: "Transacao",
                newName: "IX_Transacao_FormaPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_CategoriaId",
                table: "Transacao",
                newName: "IX_Transacao_CategoriaId");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "FormasPagamento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Transacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Transacao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacao",
                table: "Transacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcelas_Transacao_TransacaoID",
                table: "Parcelas",
                column: "TransacaoID",
                principalTable: "Transacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Categorias_CategoriaId",
                table: "Transacao",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_FormasPagamento_FormaPagamentoId",
                table: "Transacao",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_Usuarios_UsuarioId",
                table: "Transacao",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
