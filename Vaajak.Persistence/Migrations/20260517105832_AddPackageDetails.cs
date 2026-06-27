using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaajak.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Packages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Packages",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Producer",
                table: "Packages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Packages",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RateCount",
                table: "Packages",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Producer",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "RateCount",
                table: "Packages");
        }
    }
}
