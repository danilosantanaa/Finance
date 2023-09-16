using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Retrict_delete_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobrancas_Transacionadores_TransacionadorId",
                table: "Cobrancas");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobrancas_Transacionadores_TransacionadorId",
                table: "Cobrancas",
                column: "TransacionadorId",
                principalTable: "Transacionadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cobrancas_Transacionadores_TransacionadorId",
                table: "Cobrancas");

            migrationBuilder.AddForeignKey(
                name: "FK_Cobrancas_Transacionadores_TransacionadorId",
                table: "Cobrancas",
                column: "TransacionadorId",
                principalTable: "Transacionadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
