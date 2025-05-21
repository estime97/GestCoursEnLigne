using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class correction_table_requisition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BureauRequisition",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateAffichage",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateBornage",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DateInsertionJORT",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "DatePublication",
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

            migrationBuilder.DropColumn(
                name: "EquipeBornage",
                table: "Requisitions");

            migrationBuilder.DropColumn(
                name: "Geometre",
                table: "Requisitions");

            migrationBuilder.RenameColumn(
                name: "TypePrestation",
                table: "Requisitions",
                newName: "Statut");

            migrationBuilder.RenameColumn(
                name: "StatutRequisition",
                table: "Requisitions",
                newName: "PiecesManquantes");

            migrationBuilder.RenameColumn(
                name: "NumeroTitre",
                table: "Requisitions",
                newName: "NumeroTitreFoncier");

            migrationBuilder.RenameColumn(
                name: "NumeroJORT",
                table: "Requisitions",
                newName: "DateCreation");

            migrationBuilder.RenameColumn(
                name: "Localite",
                table: "Requisitions",
                newName: "Bureau");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Statut",
                table: "Requisitions",
                newName: "TypePrestation");

            migrationBuilder.RenameColumn(
                name: "PiecesManquantes",
                table: "Requisitions",
                newName: "StatutRequisition");

            migrationBuilder.RenameColumn(
                name: "NumeroTitreFoncier",
                table: "Requisitions",
                newName: "NumeroTitre");

            migrationBuilder.RenameColumn(
                name: "DateCreation",
                table: "Requisitions",
                newName: "NumeroJORT");

            migrationBuilder.RenameColumn(
                name: "Bureau",
                table: "Requisitions",
                newName: "Localite");

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
                name: "DateBornage",
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

            migrationBuilder.AddColumn<string>(
                name: "EquipeBornage",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Geometre",
                table: "Requisitions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
