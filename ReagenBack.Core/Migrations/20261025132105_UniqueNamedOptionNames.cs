using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReagenBack.Core.Migrations
{
    /// <inheritdoc />
    public partial class UniqueNamedOptionNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_UserId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Laboratories_UserId",
                table: "Laboratories");

            migrationBuilder.DropIndex(
                name: "IX_FundingSources_UserId",
                table: "FundingSources");

            migrationBuilder.DropIndex(
                name: "IX_ControlAgencies_UserId",
                table: "ControlAgencies");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserId_Name",
                table: "Suppliers",
                columns: new[] { "UserId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_UserId_Name",
                table: "Laboratories",
                columns: new[] { "UserId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FundingSources_UserId_Name",
                table: "FundingSources",
                columns: new[] { "UserId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControlAgencies_UserId_Name",
                table: "ControlAgencies",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_UserId_Name",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Laboratories_UserId_Name",
                table: "Laboratories");

            migrationBuilder.DropIndex(
                name: "IX_FundingSources_UserId_Name",
                table: "FundingSources");

            migrationBuilder.DropIndex(
                name: "IX_ControlAgencies_UserId_Name",
                table: "ControlAgencies");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_UserId",
                table: "Suppliers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_UserId",
                table: "Laboratories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSources_UserId",
                table: "FundingSources",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlAgencies_UserId",
                table: "ControlAgencies",
                column: "UserId");
        }
    }
}
