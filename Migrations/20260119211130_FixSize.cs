using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reagen_back.Migrations
{
    /// <inheritdoc />
    public partial class FixSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Reagent_Sizes",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Reagent_Sizes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Size_Amount",
                table: "Package",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Size_Unit",
                table: "Package",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Reagent_Sizes");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Reagent_Sizes");

            migrationBuilder.DropColumn(
                name: "Size_Amount",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "Size_Unit",
                table: "Package");
        }
    }
}
