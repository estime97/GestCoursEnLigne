using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_requisition_foreign_key_statut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatutRequisitionId",
                table: "Requisitions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requisitions_StatutRequisitionId",
                table: "Requisitions",
                column: "StatutRequisitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requisitions_StatutRequisitions_StatutRequisitionId",
                table: "Requisitions",
                column: "StatutRequisitionId",
                principalTable: "StatutRequisitions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requisitions_StatutRequisitions_StatutRequisitionId",
                table: "Requisitions");

            migrationBuilder.DropIndex(
                name: "IX_Requisitions_StatutRequisitionId",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "StatutRequisitionId",
                table: "Requisitions");
        }
    }
}
