using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace reagen_back.Migrations
{
    /// <inheritdoc />
    public partial class AddMainDataRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reagent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Dimension = table.Column<int>(type: "integer", nullable: false),
                    ControlAgencyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reagent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reagent_ControlAgencies_ControlAgencyId",
                        column: x => x.ControlAgencyId,
                        principalTable: "ControlAgencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Purity = table.Column<double>(type: "double precision", nullable: false),
                    InDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReagentId = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    SupplierId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Package_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Package_Reagent_ReagentId",
                        column: x => x.ReagentId,
                        principalTable: "Reagent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Package_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Reagent_Sizes",
                columns: table => new
                {
                    ReagentId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reagent_Sizes", x => new { x.ReagentId, x.Id });
                    table.ForeignKey(
                        name: "FK_Reagent_Sizes_Reagent_ReagentId",
                        column: x => x.ReagentId,
                        principalTable: "Reagent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PackageId = table.Column<int>(type: "integer", nullable: false),
                    LaboratoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vial_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vial_Package_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Package",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Package_BrandId",
                table: "Package",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Package_ReagentId",
                table: "Package",
                column: "ReagentId");

            migrationBuilder.CreateIndex(
                name: "IX_Package_SupplierId",
                table: "Package",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Reagent_ControlAgencyId",
                table: "Reagent",
                column: "ControlAgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Vial_LaboratoryId",
                table: "Vial",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vial_PackageId",
                table: "Vial",
                column: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reagent_Sizes");

            migrationBuilder.DropTable(
                name: "Vial");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "Reagent");
        }
    }
}
