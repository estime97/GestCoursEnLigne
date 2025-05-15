using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class suppression_table_statut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requisitions_StatutRequisitions_StatutRequisitionId",
                table: "Requisitions");

            migrationBuilder.DropTable(
                name: "StatutRequisitions");

            migrationBuilder.DropIndex(
                name: "IX_Requisitions_StatutRequisitionId",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "StatutRequisitionId",
                table: "Requisitions");

            migrationBuilder.AddColumn<string>(
                name: "StatutRequisition",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatutRequisition",
                table: "Requisitions");

            migrationBuilder.AddColumn<int>(
                name: "StatutRequisitionId",
                table: "Requisitions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StatutRequisitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutRequisitions", x => x.Id);
                });

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
    }
}
