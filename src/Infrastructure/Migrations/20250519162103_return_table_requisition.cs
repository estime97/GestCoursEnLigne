using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class return_table_requisition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Statut",
                table: "Requisitions",
                newName: "TypePrestation");

            migrationBuilder.RenameColumn(
                name: "Requerant",
                table: "Requisitions",
                newName: "StatutRequisition");

            migrationBuilder.RenameColumn(
                name: "PiecesManquantes",
                table: "Requisitions",
                newName: "PrenomRequerant");

            migrationBuilder.RenameColumn(
                name: "Observation",
                table: "Requisitions",
                newName: "NomRequerant");

            migrationBuilder.RenameColumn(
                name: "Motif",
                table: "Requisitions",
                newName: "MotifRejet");

            migrationBuilder.RenameColumn(
                name: "DateReception",
                table: "Requisitions",
                newName: "Localite");

            migrationBuilder.RenameColumn(
                name: "Bureau",
                table: "Requisitions",
                newName: "EquipeBornage");

            migrationBuilder.AddColumn<string>(
                name: "BureauRequisition",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateAffichage",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateInsertionJORT",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DatePublication",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateRequisition",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateRetrait",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateSigned",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTransmission",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BureauRequisition",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateAffichage",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateInsertionJORT",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DatePublication",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateRequisition",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateRetrait",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateSigned",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateTransmission",
                table: "Requisitions");

            migrationBuilder.RenameColumn(
                name: "TypePrestation",
                table: "Requisitions",
                newName: "Statut");

            migrationBuilder.RenameColumn(
                name: "StatutRequisition",
                table: "Requisitions",
                newName: "Requerant");

            migrationBuilder.RenameColumn(
                name: "PrenomRequerant",
                table: "Requisitions",
                newName: "PiecesManquantes");

            migrationBuilder.RenameColumn(
                name: "NomRequerant",
                table: "Requisitions",
                newName: "Observation");

            migrationBuilder.RenameColumn(
                name: "MotifRejet",
                table: "Requisitions",
                newName: "Motif");

            migrationBuilder.RenameColumn(
                name: "Localite",
                table: "Requisitions",
                newName: "DateReception");

            migrationBuilder.RenameColumn(
                name: "EquipeBornage",
                table: "Requisitions",
                newName: "Bureau");
        }
    }
}
