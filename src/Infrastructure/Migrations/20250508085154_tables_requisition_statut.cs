using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tables_requisition_statut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requisitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroRequisition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomRequerant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrenomRequerant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRequisition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BureauRequisition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geometre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBornage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipeBornage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTransmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypePrestation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroJORT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateInsertionJORT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAffichage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePublication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroTitre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSigned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRetrait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatutRequisitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutRequisitions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requisitions");

            migrationBuilder.DropTable(
                name: "StatutRequisitions");
        }
    }
}
